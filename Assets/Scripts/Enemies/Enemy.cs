using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Moving
{

    //movement stuff
    public int direction = -1; //starts walking left
    public float maxDist = 10f;
    public float minDist = 0f;

    //stats
    protected float damagePerHit; //based on subclass and level

    //conditions
    protected bool sleeping = false;
    protected bool frozen = false;
    protected bool scared = false;
    private bool illuminated = false;

    Player player;

    new void Start()
    {
        base.Start();
        Game.AddEnemyToList(this);

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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == Game.playerTag)
        {
            Attack(other.gameObject.GetComponent<Player>());
        }
    }

    void Update()//or FixedUpdate?
    {
        Act();
    }

    public override void PlayAttackAnimation()
    {
        animator.SetTrigger(Moving.attackAnimation);
    }

    //TODO: other animation things 

    public bool IsIlluminated()
    {
        return illuminated;
    }

    public void IllumiateOn()
    {
        illuminated = true;
        GetComponent<Renderer>().enabled = true;
    }

    public void IlluminateOff()
    {
        illuminated = false;
        GetComponent<Renderer>().enabled = false;
    }

    public void DecreaseLevel(int amount)
    {
        level = level - amount;
    }

    public void Sleep()
    {
        sleeping = true;
    }

    public void Wake()
    {
        sleeping = false;
    }

    public bool IsSleeping()
    {
        return sleeping;
    }

    public void Freeze()
    {
        frozen = true;
    }

    public void Unfreeze()
    {
        frozen = false;
    }

    public bool IsFrozen()
    {
        return frozen;
    }
    public void Scare()
    {
        scared = true;
    }

    public void NoLongerScared()
    {
        scared = false;
    }

    public bool IsScared()
    {
        return scared;
    }

    public void Attack(Player player)
    {
        PlayAttackAnimation();
        player.TakeDamage(damagePerHit);
        DisplayPlayerHealth.UpdateHealthDisplay();
    }

    //https://forum.unity3d.com/threads/left-and-right-enemy-moving-in-2d-platformer.364716/
    protected void FollowPath()
    {
        float x = transform.position.x;
        switch (direction)
        {
            case -1:
                if (x > minDist)
                {
                    x = -1;
                }
                else
                {
                    direction = 1;
                    Flip();
                }
                break;
            case 1:
                if (x < maxDist)
                {
                    x = 1;
                }
                else
                {
                    direction = -1;
                    Flip();
                }
                break;
        }
        rb2D.velocity = new Vector2(x * speed, transform.position.y);
       // transform.position = new Vector2(x, );
    }

    public void Act()
    { //TODO: please 
        if (InRange())
        {
            MoveToPlayer();
        }
        else
        {
            FollowPath();
            //TODO: follow a path, flip at walls
            // int x = 0;
            // int y = 0;
            // System.Random random = new System.Random();
            // x = random.Next(-2, 2);
            // if (x == 0)
            // {
            //     y = random.Next(-2, 2);
            // }
            // Move(x, y);

            //if (Game.GetVisibleSpots().Contains(location)) {
            // enemy is visible
            //	IllumiateOn();
            //} else {
            //	IlluminateOff();
            //}
        }
    }

    public bool InRange()
    { //TODO: please 
      //checks if the enemy is close enough to the player/should move
      //to attack or get closer
        return false;
    }

    //https://unity3d.com/learn/tutorials/topics/2d-game-creation/top-down-2d-game-basics?playlist=17093
    public void MoveToPlayer()
    { 
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
        rb2D.AddForce(gameObject.transform.up * speed);
    }
}
