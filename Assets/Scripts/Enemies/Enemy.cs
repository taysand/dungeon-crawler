using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Moving
{

    //movement stuff
    public int direction = -1; //starts walking left
    public float maxDist = 10;
    public float minDist = 0;
    protected float speed;

    //stats
    protected float damagePerHit; //based on subclass and level

    //conditions
    protected bool sleeping = false;
    protected bool frozen = false;
    protected bool scared = false;
    private bool illuminated = false;

    void FixedUpdate()
    {
        Act();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Attack(other.gameObject.GetComponent<Player>());
        }
    }

    public override void PlayAttackAnimation() {
        animator.SetTrigger("Attack");
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

    public bool Frozen()
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

    public bool Scared()
    {
        return scared;
    }

    public void Attack(Player player)
    {
        PlayAttackAnimation();
        player.TakeDamage(damagePerHit);
        DisplayPlayerHealth.UpdateHealthDisplay();
    }

    new protected void Move()
    {
        base.Move();
        FollowPath();

        Vector3 location = transform.position;

        //if (Game.GetVisibleSpots().Contains(location)) {
        // enemy is visible
        //	IllumiateOn();
        //} else {
        //	IlluminateOff();
        //}
    }

    protected void FollowPath()
    {
        float x = transform.position.x;
        switch (direction)
        {
            case -1:
                if (x > minDist)
                {
                    x -= speed;
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
                    x += speed;
                }
                else
                {
                    direction = -1;
                    Flip();
                }
                break;
        }
        transform.localPosition = new Vector2(x, transform.position.y);
    }

    public void Act()
    { //TODO: please 
        if (InRange())
        {
            //if next to player, then attack
            //else movetoplayer
        }
        else
        {
            Move();
        }
    }

    public bool InRange()
    { //TODO: please 
      //checks if the enemy is close enough to the player/should move
      //to attack or get closer
        return false;
    }

    public void MoveToPlayer()
    { //TODO: please 
      //move toward player
    }
}
