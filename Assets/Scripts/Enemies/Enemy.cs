using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Player player;

    //movement
    protected Vector2 startingLocation;//starting location
    public Vector2 endLocation;//ending location, set per individual 
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

        startingLocation = transform.position;
        StartFollowingPath();
        movingToPlayer = false;
        //do like a distance from it's "starting point" which can change?
    }

    private void StartFollowingPath()
    {
        StartCoroutine(followPath);
        followingPath = true;
    }

    private void StopFollowingPath()
    {
        StopCoroutine(followPath);
        followingPath = false;
    }

    void Update()//FIXME: delete after tests
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("freezing");
            StartCoroutine(Freeze());
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("sleeping");
            StartCoroutine(Sleep());
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("scared");
            StartCoroutine(Scare());
        }
    }

    IEnumerator FollowPath()
    {
        if (facingRight)
        {
            while (Vector2.Distance(transform.position, startingLocation) > .05f)
            {
                transform.position = Vector2.Lerp(transform.position, startingLocation, speed * Time.deltaTime);
                yield return null;
            }
            Flip();
        }
        else
        {
            while (Vector2.Distance(transform.position, endLocation) > .05f)
            {
                transform.position = Vector2.Lerp(transform.position, endLocation, speed * Time.deltaTime);
                yield return null;
            }
            Flip();
        }
        StartCoroutine(followPath);
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
            inRange = true;
            if (!frozen && !sleeping && !scared)
            {
                StopFollowingPath();
                StartMovingToPlayer();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == Game.playerTag) && !followingPath)
        {
            inRange = false;
            if (!frozen && !sleeping && !scared)
            {
                StopMovingToPlayer();
                StartFollowingPath();
            }
        }
    }

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

    public IEnumerator Sleep()
    {
        sleeping = true;
        //TODO: play sleep animation 
        StopMovement();
        yield return new WaitForSeconds(sleepTime);
        WakeUp();
    }

    public void WakeUp()
    {
        sleeping = false;
        //TODO: play wake animation 
        StartMovement();
    }

    public bool IsSleeping()
    {
        return sleeping;
    }

    public IEnumerator Freeze()
    {
        frozen = true;
        //TODO: play freeze animation
        StopMovement();
        yield return new WaitForSeconds(freezeTime);
        Unfreeze();
    }

    public void Unfreeze()
    {
        frozen = false;
        //TODO: play unfreeze animation
        StartMovement();
    }

    public void StartMovement() {
        if (inRange)
        {
            StartMovingToPlayer();
        }
        else
        {
            StartFollowingPath();
        }
    }

    public void StopMovement() {
        if (movingToPlayer)
        {
            StopMovingToPlayer();
        }
        else
        {
            StopFollowingPath();
        }
    }

    public bool IsFrozen()
    {
        return frozen;
    }

    public IEnumerator Scare()
    {
        scared = true;
        //TODO: play scared animation

        StopMovement();

        while (Vector2.Distance(transform.position, player.transform.position) < scaredDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
            // if (oldX - transform.position.x) { TODO:
            //     Flip();
            // }
            yield return null;
        }

        yield return new WaitForSeconds(scaredTime);
        NoLongerScared();
    }

    public void NoLongerScared()
    {
        scared = false;
        //TODO: play no longer scared animation
        StartMovement();
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

    private void StartMovingToPlayer()
    {
        StartCoroutine(moveToPlayer);
        movingToPlayer = true;
    }

    private void StopMovingToPlayer()
    {
        StopCoroutine(moveToPlayer);
        movingToPlayer = false;
    }

    IEnumerator MoveToPlayer()
    {
        //float oldX = transform.position.x;
        while (Vector2.Distance(transform.position, player.transform.position) > .5f)
        {
            transform.position = Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);
            // if (oldX - transform.position.x) { TODO:
            //     Flip();
            // }
            yield return null;
        }

        //FIXME: probably delete 
        //https://unity3d.com/learn/tutorials/topics/2d-game-creation/top-down-2d-game-basics?playlist=17093 
        // float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        // transform.eulerAngles = new Vector3(0, 0, z);
        // rb2D.AddForce(gameObject.transform.up * speed * 10);
    }
}
