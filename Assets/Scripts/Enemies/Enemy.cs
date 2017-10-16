using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : Moving
{
    //coroutine stuff from https://unity3d.com/learn/tutorials/topics/scripting/coroutines?playlist=17117

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

    protected override void Start()
    {
        base.Start();
        Game.AddEnemyToList(this);

        GetComponent<CircleCollider2D>().radius = rangeRadius;
        facingRight = false;

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
        movingToEnd = true;
        movingToPlayer = false;

        //part of attempting turn based
        // target = GameObject.FindGameObjectWithTag("Player").transform;
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
            // inRange = true;
            // if (!frozen && !sleeping && !scared)
            // {
                // StopFollowingPath();
                // StartMovingToPlayer();
                movingToPlayer = true;
                // Debug.Log("movingToPlayer is now " + movingToPlayer);
            // }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit trigger");
        if ((other.gameObject.tag == Game.playerTag)) //&& !followingPath)
        {
            // Debug.Log("no longer moving to player");
            // if (!frozen && !sleeping && !scared)
            // {
                movingToPlayer = false;
                // Debug.Log("movingToPlayer is now " + movingToPlayer);
            // }
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
        // shouldMove = false;//TODO: do I need this
        Debug.Log("sleeping");
        sleeping = true;
        turnsToSleep = sleepTime + additionalSleepTurns;
        //TODO: play sleep animation 
    }

    public void WakeUp()
    {
        Debug.Log("awake");
        turnsSpentSleeping = 0;
        sleeping = false;
        // shouldMove = true;
        //TODO: play wake animation 
    }

    public bool IsSleeping()
    {
        return sleeping;
    }

    public void Freeze(int additionalFreezeTime)
    {
        // shouldMove = false;
        Debug.Log("frozen");
        frozen = true;
        turnsToFreeze = freezeTime + additionalFreezeTime;
        //TODO: play freeze animation 
    }

    public void Unfreeze()
    {
        Debug.Log("unfrozen");
        turnsSpentFrozen = 0;
        frozen = false;
        //TODO: play unfreeze animation
        // shouldMove = true;
    }

    public bool IsFrozen()
    {
        return frozen;
    }

    // public bool ShouldMove()
    // {
    //     return shouldMove;
    // }

    public void Scare(int additionalScareTime, int additionalScareDistance)
    {
        Debug.Log("scared");
        // shouldMove = false;
        scared = true;
        turnsToScare = scaredTime + additionalScareTime;
        //TODO: play scared animation
        //TODO: move the enemy away from the player, for the set time and distance


        // StopMovement();

        // while (Vector2.Distance(transform.position, player.transform.position) < scaredDistance + additionalScareDistance)
        // {
        //     //http://answers.unity3d.com/questions/1137454/gameobject1-move-away-when-gameobject2-gets-close.html
        //     transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
        //     if (oldX - transform.position.x)
        //     {

        //         Flip();
        //     }
        //     yield return null;
        // }

        // yield return new WaitForSeconds(scaredTime + additionalScareTime);
        // NoLongerScared();
    }

    public void NoLongerScared()
    {
        // shouldMove = true;
        Debug.Log("no longer scared");
        scared = false;
        turnsSpentScared = 0;
        //TODO: play no longer scared animation
    }

    public bool IsScared()
    {
        return scared;
    }

    public void Attack(Player player)
    {
        PlayAttackAnimation();
        if (!Game.IsPlayersTurn())
        {
            player.TakeDamage(damagePerHit);
        }
    }

    //FIXME: attempting turn based below
    // private Transform target;
    int yDirection;
    int xDirection;
    bool lastMovedX;
    bool movingToEnd;

    private void SetDirections()
    {
        // Debug.Log("setting direction");

        if (scared)
        {
            Debug.Log("scared movement");
            if (connectedJoint.position.x > player.connectedJoint.transform.position.x)
            {
                xDirection = 1;
            }
            else
            {
                xDirection = -1;
            }

            if (connectedJoint.position.y > player.connectedJoint.transform.position.y)
            {
                yDirection = 1;
            }
            else
            {
                yDirection = -1;
            }
            turnsSpentScared++;
            if (turnsSpentScared >= turnsToScare) {
                Debug.Log("done with scared");
                NoLongerScared();
            }
        }
        else if (movingToPlayer)
        {
            Debug.Log("moving to player and player is at " + player.connectedJoint.transform.position);
            if (connectedJoint.position.x > player.connectedJoint.transform.position.x)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }

            if (connectedJoint.position.y > player.connectedJoint.transform.position.y)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else if (movingToEnd)
        {
            Debug.Log("moving to end and end is " + endLocation);
            if (connectedJoint.position.x > endLocation.x)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }

            if (connectedJoint.position.y > endLocation.y)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else if (!movingToEnd)
        {
            Debug.Log("moving to start and start is " + startingLocation);
            if (connectedJoint.position.x < startingLocation.x)
            {
                xDirection = 1;
            }
            else
            {
                xDirection = -1;
            }

            if (connectedJoint.position.y < startingLocation.y)
            {
                yDirection = 1;
            }
            else
            {
                yDirection = -1;
            }
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

        // if (scared)
        // {
        //     if (turnsSpentScared < turnsToScare)
        //     {
        //         turnsSpentScared++;
        //         return;
        //     }
        //     else
        //     {
        //         NoLongerScared();
        //     }
        // }

        // Debug.Log(transform.GetComponent<Enemy>() + " got to here so the enemy better not be frozen or sleeping");
        Vector2 movement;
        SetDirections();

        movement = MoveOne();

        if (Math.Abs(connectedJoint.position.x - endLocation.x) < 1 && Math.Abs(connectedJoint.position.y - endLocation.y) < 1 && movingToEnd)
        {
            // Debug.Log("reached the end positiion");
            //turn around and go back to startLocation
            // SetDirections();
            Flip();
            movingToEnd = false;
        }
        else if (Math.Abs(connectedJoint.position.x - startingLocation.x) < 1 && Math.Abs(connectedJoint.position.y - startingLocation.y) < 1 && !movingToEnd)
        {
            // Debug.Log("reached the start positiion");
            //turn around and go back to endLocation
            // SetDirections();
            Flip();
            movingToEnd = true;
        }
        // Debug.Log("moving to end? " + movingToEnd);

        // if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        // { //are enemy and player in the same column 
        // Debug.Log("enemy and player are in the same column? so it's tracking you");
        //     yDir = target.position.y > transform.position.y ? 1 : -1;
        // }
        // else
        // {
        //     xDir = target.position.x > transform.position.x ? 1 : -1;
        // }

        AttemptMove<Player>((int)movement.x, (int)movement.y);
        // Debug.Log("now enemy is at " + transform.position);
    }

    // //https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial
    // protected override void OnCantMove<T>(T component)
    // {
    //     Player hitPlayer = component as Player;
    //     animator.SetTrigger("EnemyAttack");
    //     hitPlayer.TakeDamage(damagePerHit);
    // }
}
