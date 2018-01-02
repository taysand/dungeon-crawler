using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : Moving
{
    //coroutine stuff from https://unity3d.com/learn/tutorials/topics/scripting/coroutines?playlist=17117
    //turn based stuff from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial

    //stats
    protected float damagePerHit;
    protected float rangeRadius;
    protected int sleepTime;
    protected int freezeTime;
    protected int scaredTime;

    //conditions
    private bool sleeping = false;
    private bool frozen = false;
    private bool scared = false;
    private int turnsSpentSleeping = 0;
    private int turnsSpentFrozen = 0;
    private int turnsSpentScared = 0;
    private int turnsToSleep;
    private int turnsToFreeze;
    private int turnsToScare;

    Player player;

    //movement
    public Vector3 startingLocation;
    public Vector3 endLocation;
    private bool movingToPlayer;
    private int yDirection;
    private int xDirection;
    private bool lastMovedX;
    private bool movingToEnd;

    private bool facingRight;
    private bool lastRight;

    protected override void Start()
    {
        base.Start();
        Game.AddEnemyToList(this);

        GetComponent<CircleCollider2D>().radius = rangeRadius;

        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        }

        transform.position = startingLocation;
        lastMovedX = false;
        movingToPlayer = false;
        movingToEnd = true;
        facingRight = spriteRenderer.flipX;
        Debug.Log("facing right is " + facingRight);
        lastRight = facingRight;
    }

    void Update()
    {
        if (lastRight != facingRight)
        {
            spriteRenderer.flipX = facingRight;
            lastRight = facingRight;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Game.playerTag)
        {
            if (!frozen && !sleeping && !scared)
            {
                Attack(other.gameObject.GetComponent<Player>());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == Game.playerTag) && !movingToPlayer)
        {
            movingToPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == Game.playerTag))
        {
            movingToPlayer = false;
        }
    }

    public override void PlayAttackAnimation()
    {
        animator.SetTrigger(Moving.attackAnimation);
    }

    //TODO: other animation things 

    public void DecreaseLevel(int amount)
    {
        level = level - amount;
    }

    public void Sleep(int additionalSleepTurns)
    {
        Debug.Log("sleeping");
        sleeping = true;
        turnsToSleep = sleepTime + additionalSleepTurns;
        //TODO: play sleep animation 
    }

    public void WakeUp()
    {
        // Debug.Log("awake");
        turnsSpentSleeping = 0;
        sleeping = false;
        //TODO: play wake animation 
    }

    public void Freeze(int additionalFreezeTime)
    {
        // Debug.Log("frozen");
        frozen = true;
        turnsToFreeze = freezeTime + additionalFreezeTime;
        //TODO: play freeze animation 
    }

    public void Unfreeze()
    {
        // Debug.Log("unfrozen");
        turnsSpentFrozen = 0;
        frozen = false;
        //TODO: play unfreeze animation
    }

    public void Scare(int additionalScareTime, int additionalScareDistance)
    {
        // Debug.Log("enemy is " + transform.GetComponent<Enemy>() + "is now scared");
        scared = true;
        turnsToScare = scaredTime + additionalScareTime;
        //TODO: play scared animation
    }

    public void NoLongerScared()
    {
        // Debug.Log("no longer scared");
        scared = false;
        turnsSpentScared = 0;
        //TODO: play no longer scared animation
    }

    public void Attack(Player player)
    {
        PlayAttackAnimation();
        if (!Game.IsPlayersTurn())
        {
            player.TakeDamage(damagePerHit);
        }
    }

    private void SetDirections()
    {
        if (scared)
        {
            // Debug.Log("scared movement");
            DetailSetDirection(player.connectedJoint.transform.position, false);
            turnsSpentScared++;
            if (turnsSpentScared >= turnsToScare)
            {
                NoLongerScared();
            }
        }
        else if (movingToPlayer)
        {
            Debug.Log("moving to player and player is at " + player.connectedJoint.transform.position);
            DetailSetDirection(player.connectedJoint.transform.position, true);
        }
        else if (movingToEnd)
        {
            Debug.Log("moving to end and end is " + endLocation);
            DetailSetDirection(endLocation, true);
        }
        else if (!movingToEnd)
        {
            DetailSetDirection(startingLocation, true);
            Debug.Log("moving to start and start is " + startingLocation);
        }
    }

    private void DetailSetDirection(Vector2 goal, bool movingTo)
    {
        int direction = 1;
        if (!movingTo)
        {
            direction = -1;
        }
        if (connectedJoint.position.x > goal.x)
        {
            xDirection = -1 * direction;
            if (direction == 1)
            {
                if (facingRight)
                {
                    Debug.Log("flipping left");
                    lastRight = true;
                }
                facingRight = false;
            }
            else
            {
                if (!facingRight)
                {
                    Debug.Log("flipping right");
                    lastRight = false;
                }
                facingRight = true;
            }
        }
        else
        {
            xDirection = 1 * direction;
            if (direction == 1)
            {
                if (!facingRight)
                {
                    Debug.Log("flipping right");
                    lastRight = false;
                }
                facingRight = true;
            }
            else
            {
                if (facingRight)
                {
                    Debug.Log("flipping left");
                    lastRight = true;
                }
                facingRight = false;
            }
        }

        if (connectedJoint.position.y > goal.y)
        {
            yDirection = -1 * direction;
        }
        else
        {
            yDirection = 1 * direction;
        }
    }

    private Vector2 MoveOne()
    {
        int x = 0;
        int y = 0;
        if (lastMovedX)
        {
            y = 1 * yDirection;
            // Debug.Log("moving y");
            lastMovedX = false;
        }
        else
        {
            x = 1 * xDirection;
            // Debug.Log("moving x");
            lastMovedX = true;
        }
        return new Vector2(x, y);
    }

    public void MoveEnemy()
    {
        // Debug.Log(transform.GetComponent<Enemy>() + " is sleeping: " + sleeping);
        if (sleeping)
        {
            if (turnsSpentSleeping < turnsToSleep)
            {
                // Debug.Log(transform.GetComponent<Enemy>() + " sleeping");
                turnsSpentSleeping++;
                return;
            }
            else
            {
                // Debug.Log(transform.GetComponent<Enemy>() + " about to wake up");
                WakeUp();
            }

        }
        // Debug.Log(transform.GetComponent<Enemy>() + " is frozen: " + frozen);
        if (frozen)
        {
            if (turnsSpentFrozen < turnsToFreeze)
            {
                // Debug.Log(transform.GetComponent<Enemy>() + " frozen");
                turnsSpentFrozen++;
                return;
            }
            else
            {
                // Debug.Log(transform.GetComponent<Enemy>() + " about to unfreeze");
                Unfreeze();
            }
        }

        // Debug.Log(transform.GetComponent<Enemy>() + " got to here so the enemy better not be frozen or sleeping");
        Vector2 movement;
        SetDirections();

        movement = MoveOne();

        if (Math.Abs(connectedJoint.position.x - endLocation.x) < 1 && Math.Abs(connectedJoint.position.y - endLocation.y) < 1 && movingToEnd)
        {
            Debug.Log("reached the end position");
            // if (!movingToPlayer)
            // {
            //     Flip();
            // }
            movingToEnd = false;
        }
        else if (Math.Abs(connectedJoint.position.x - startingLocation.x) < 1 && Math.Abs(connectedJoint.position.y - startingLocation.y) < 1 && !movingToEnd)
        {
            Debug.Log("reached the start position");
            // if (!movingToPlayer)
            // {
            //     Flip();
            // }
            movingToEnd = true;
        }

        Move((int)movement.x, (int)movement.y);
    }

    public void Teleport(int x, int y)
    {
        Move(x, y);
    }

    // private void UpdateFlipped() {
    //     if (facingRight) {
    //         facingRight = !facingRight;
    //     }
    // }
}
