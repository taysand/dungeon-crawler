using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : Moving
{
    //coroutine stuff from https://unity3d.com/learn/tutorials/topics/scripting/coroutines?playlist=17117

    //movement stuff FIXME: probably delete
    // public int direction = -1; //starts walking left
    // public float maxDist = 10f;
    // public float minDist = 0f;

    //stats
    protected float damagePerHit;
    protected float rangeRadius;
    protected int sleepTime;
    protected int freezeTime;
    protected int scaredTime;
    protected int scaredDistance;

    //conditions
    protected bool sleeping = false;
    protected bool frozen = false;
    protected bool scared = false;
    private bool illuminated = false;
    private bool shouldMove;

    Player player;

    //movement
    public Vector3 startingLocation;//starting location, set per individual 
    public Vector3 endLocation;//ending location, set per individual 
    private bool followingPath;
    private bool movingToPlayer;
    protected bool inRange;

    // Coroutine names
    private const string followPath = "FollowPath";
    private const string moveToPlayer = "MoveToPlayer";

    protected override void Start()
    {
        base.Start();
        Game.AddEnemyToList(this);

        GetComponent<CircleCollider2D>().radius = rangeRadius;
        facingRight = false;
        //inRange = false; FIXME: probably delete

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
        shouldMove = true;
        movingToEnd = true;
        // StartFollowingPath();
        movingToPlayer = false;
        //do like a distance from it's "starting point" which can change?

        //part of attempting turn based
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // private void StartFollowingPath()
    // {
    //     StartCoroutine(followPath);
    //     followingPath = true;
    // }

    // private void StopFollowingPath()
    // {
    //     StopCoroutine(followPath);
    //     followingPath = false;
    // }

    // IEnumerator FollowPath()
    // {
    //     if (facingRight)
    //     {
    //         while (Vector2.Distance(transform.position, startingLocation) > .05f)
    //         {
    //             transform.position = Vector2.Lerp(transform.position, startingLocation, speed * Time.deltaTime);
    //             yield return null;
    //         }
    //         Flip();
    //     }
    //     else
    //     {
    //         while (Vector2.Distance(transform.position, endLocation) > .05f)
    //         {
    //             transform.position = Vector2.Lerp(transform.position, endLocation, speed * Time.deltaTime);
    //             yield return null;
    //         }
    //         Flip();
    //     }
    //     StartCoroutine(followPath);
    // }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == Game.playerTag)
    //     {
    //         if (!frozen && !sleeping && !scared)
    //         {
    //             Attack(other.gameObject.GetComponent<Player>());
    //         }
    //     }
    // }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if ((other.gameObject.tag == Game.playerTag) && !movingToPlayer)
    //     {
    //         inRange = true;
    //         if (!frozen && !sleeping && !scared)
    //         {
    //             StopFollowingPath();
    //             StartMovingToPlayer();
    //         }
    //     }
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if ((other.gameObject.tag == Game.playerTag) && !followingPath)
    //     {
    //         inRange = false;
    //         if (!frozen && !sleeping && !scared)
    //         {
    //             StopMovingToPlayer();
    //             StartFollowingPath();
    //         }
    //     }
    // }

    // void FixedUpdate() FIXME: probably delete
    // {
    //    // InRange();
    // }

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

    // public IEnumerator Sleep(float additionalSleepTime)
    // {
    //     shouldMove = false;
    //     Debug.Log("sleeping");
    //     sleeping = true;
    //     //TODO: play sleep animation 
    //     StopMovement();
    //     yield return new WaitForSeconds(sleepTime + additionalSleepTime);
    //     WakeUp();
    // }

    // public void WakeUp()
    // {
    //     Debug.Log("awake");
    //     sleeping = false;
    //     shouldMove = true;
    //     //TODO: play wake animation 
    //     StartMovement();
    // }

    public bool IsSleeping()
    {
        return sleeping;
    }

    // public IEnumerator Freeze(float additionalFreezeTime)
    // {
    //     shouldMove = false;
    //     Debug.Log("following path should be false and is: " + followingPath);
    //     Debug.Log("frozen = " + frozen);
    //     Debug.Log("about to freeze");
    //     frozen = true;
    //     Debug.Log("frozen = " + frozen);
    //     //TODO: play freeze animation
    //     StopMovement();
    //     Debug.Log("about to wait for " + freezeTime + " + " + additionalFreezeTime);
    //     //the game unpauses here, which means the enemy starts moving again, before it should
    //     yield return new WaitForSeconds(freezeTime + additionalFreezeTime);
    //     Debug.Log("entering Unfreeze");
    //     //enemy is moving at this point, when it shouldn't be
    //     Unfreeze();
    // }

    // public void Unfreeze()
    // {
    //     Debug.Log("following path should be false and is: " + followingPath);
    //     Debug.Log("frozen = " + frozen);
    //     Debug.Log("about to unfreeze");
    //     frozen = false;
    //     Debug.Log("frozen = " + frozen);
    //     //TODO: play unfreeze animation
    //     shouldMove = true;
    //     StartMovement();
    // }

    // public void StartMovement()
    // {
    //     Debug.Log("starting movement");
    //     if (inRange)
    //     {
    //         StartMovingToPlayer();
    //         Debug.Log("movingToPlayer should now be true and is: " + movingToPlayer);
    //     }
    //     else
    //     {
    //         StartFollowingPath();
    //         Debug.Log("followingPath should now be true and is: " + followingPath);
    //     }
    // }

    // public void StopMovement()
    // {
    //     Debug.Log("stopping movement");
    //     if (movingToPlayer)
    //     {
    //         StopMovingToPlayer();
    //         Debug.Log("movingToPlayer should now be false and is: " + movingToPlayer);
    //     }
    //     else
    //     {
    //         StopFollowingPath();
    //         Debug.Log("followingPath should now be false and is: " + followingPath);
    //     }
    // }

    public bool IsFrozen()
    {
        return frozen;
    }

    public bool ShouldMove()
    {
        return shouldMove;
    }

    // public IEnumerator Scare(float additionalScareTime, int additionalScareDistance)
    // {
    //     Debug.Log("scared");
    //     shouldMove = false;
    //     scared = true;
    //     //TODO: play scared animation

    //     StopMovement();

    //     while (Vector2.Distance(transform.position, player.transform.position) < scaredDistance + additionalScareDistance)
    //     {
    //         //http://answers.unity3d.com/questions/1137454/gameobject1-move-away-when-gameobject2-gets-close.html
    //         transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
    //         // if (oldX - transform.position.x) { TODO:
    //         //     Flip();
    //         // }
    //         yield return null;
    //     }

    //     yield return new WaitForSeconds(scaredTime + additionalScareTime);
    //     NoLongerScared();
    // }

    // public void NoLongerScared()
    // {
    //     shouldMove = true;
    //     Debug.Log("no longer scared");
    //     scared = false;
    //     //TODO: play no longer scared animation
    //     StartMovement();
    // }

    public bool IsScared()
    {
        return scared;
    }

    public void Attack(Player player)
    {
        PlayAttackAnimation();
        player.TakeDamage(damagePerHit);
    }

    //https://forum.unity3d.com/threads/left-and-right-enemy-moving-in-2d-platformer.364716/ FIXME: probably delete
    // protected void FollowPath()
    // {
    //     float x = transform.position.x;
    //     switch (direction)
    //     {
    //         case -1:
    //             if (x > minDist)
    //             {
    //                 x = -1;
    //             }
    //             else
    //             {
    //                 direction = 1;
    //                 Flip();
    //             }
    //             break;
    //         case 1:
    //             if (x < maxDist)
    //             {
    //                 x = 1;
    //             }
    //             else
    //             {
    //                 direction = -1;
    //                 Flip();
    //             }
    //             break;
    //     }
    //     rb2D.velocity = new Vector2(x * speed, transform.position.y);
    //     // transform.position = new Vector2(x, );
    // }

    // public void Act() FIXME: probably delete
    // { //TODO: please 
    //     if (InRange())
    //     {
    //         MoveToPlayer();
    //     }
    //     else
    //     {
    //         FollowPath();
    //         //TODO: follow a path, flip at walls
    //         // int x = 0;
    //         // int y = 0;
    //         // System.Random random = new System.Random();
    //         // x = random.Next(-2, 2);
    //         // if (x == 0)
    //         // {
    //         //     y = random.Next(-2, 2);
    //         // }
    //         // Move(x, y);
    //         
    //     }
    // }

    //if (Game.GetVisibleSpots().Contains(location)) { TODO:? can I just use Unity's lighting for this? like give the torch the only source of light and make everything else dark?
    //         // enemy is visible
    //         //	IllumiateOn();
    //         //} else {
    //         //	IlluminateOff();
    //         //}

    // public void InRange()//could just be OnCollision2D? and then nothing in Update? FIXME: probably delete
    // {
    //     if (inRange && !movingToPlayer)
    //     {
    //         StopFollowingPath();
    //         StartMovingToPlayer();
    //     }
    //     if (!inRange && !followingPath)
    //     {
    //         // startingLocation = transform.position;
    //         StopMovingToPlayer();
    //         StartFollowingPath();
    //     }
    // }

    // private void StartMovingToPlayer()
    // {
    //     StartCoroutine(moveToPlayer);
    //     movingToPlayer = true;
    // }

    // private void StopMovingToPlayer()
    // {
    //     StopCoroutine(moveToPlayer);
    //     movingToPlayer = false;
    // }

    // IEnumerator MoveToPlayer()
    // {
    //     Debug.Log("moving to player");
    //     //float oldX = transform.position.x;
    //     while (Vector2.Distance(transform.position, player.transform.position) > .5f)
    //     {
    //         transform.position = Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
    //         // if (oldX - transform.position.x) { TODO:
    //         //     Flip();
    //         // }
    //         yield return null;
    //     }

    //     //FIXME: probably delete 
    //     //https://unity3d.com/learn/tutorials/topics/2d-game-creation/top-down-2d-game-basics?playlist=17093 
    //     // float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
    //     // transform.eulerAngles = new Vector3(0, 0, z);
    //     // rb2D.AddForce(gameObject.transform.up * speed * 10);
    // }

    //FIXME: attempting turn based below
    private Transform target;
    int yDirection;
    int xDirection;
    bool lastMovedX;
    bool movingToEnd;

    // private bool skipMove;

    // protected override void AttemptMove<T>(int xDir, int yDir)
    // {
    //     // if (skipMove)
    //     // {
    //     //     skipMove = false;
    //     //     return;
    //     // }

    //     base.AttemptMove<T>(xDir, yDir);

    //     // skipMove = true;
    // }

    private void SetDirections()
    {
        // Debug.Log("setting direction");

        if (movingToEnd)
        {
            if (transform.position.x > endLocation.x)
            {
                xDirection = -1;
            }
            else
            {
                xDirection = 1;
            }

            if (transform.position.y > endLocation.y)
            {
                yDirection = -1;
            }
            else
            {
                yDirection = 1;
            }
        }
        else
        {
            if (transform.position.x < startingLocation.x)
            {
                xDirection = 1;
            }
            else
            {
                xDirection = -1;
            }

            if (transform.position.y < startingLocation.y)
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
        Vector2 movement;
        SetDirections();

        // Debug.Log("enemy is at " + transform.position);

        // if (movingToEnd)
        // {



        movement = MoveOne();

        // }
        // else
        // {
        //     //move to endLocation
        //     Debug.Log("moving to the end, which is at " + endLocation);
        //     movement = MoveOne();
        // }
        if (Math.Abs(transform.position.x - endLocation.x) < 1 && Math.Abs(transform.position.y - endLocation.y) < 1 && movingToEnd)
        {
            // Debug.Log("reached the end positiion");
            //turn around and go back to startLocation
            // SetDirections();
            Flip();
            movingToEnd = false;
        }
        else if (Math.Abs(transform.position.x - startingLocation.x) < 1 && Math.Abs(transform.position.y - startingLocation.y) < 1 && !movingToEnd)
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

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;
        animator.SetTrigger("EnemyAttack");
        hitPlayer.TakeDamage(damagePerHit);
    }
}
