﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement code in Moving and Player partially borrowed from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/moving-object-script?playlist=17150 TODO: delete this if I don't do turn based
public abstract class Moving : MonoBehaviour
{
    public const string playerInjuredAnimation = "PlayerInjured";
    public const string attackAnimation = "Attack";

    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    protected bool facingRight;
    protected SpriteRenderer sr;

    //moving stuff, this stuff is from the tutorial TODO: delete this if I don't do turn based
    // private float inverseMoveTime;
    // public float moveTime = 0.1f;

    //stats
    protected int level;
    protected float hp;
    protected int ac;
    protected float maxHP;
    protected float speed;

    protected virtual void Start()
    {
        SetStartingValues();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //inverseMoveTime = 1f / moveTime; //this stuff is from the tutorial  TODO: delete this if I don't do turn based
        rb2D.drag = 5;
    }

    protected abstract void SetStartingValues();

    public abstract void PlayAttackAnimation();

    protected void Flip()
    {
        facingRight = !facingRight;
        sr.flipX = facingRight;
    }

    // public void Move(int x, int y) TODO: delete this if I don't do turn based
    // {
    //     Vector2 start = transform.position;
    //     Vector2 end = start + new Vector2(x, y);
    //     //StartCoroutine(SmoothMovement(end)); //this stuff is from the tutorial 
    //     transform.position = end;
    // }

    //TODO: delete this if I don't do turn based
    //fixes the wall clipping problem, but now they get stuck in walls. also enemies don't have good movement 
    // protected IEnumerator SmoothMovement(Vector3 end)
    // { //this stuff is from the tutorial 
    //     float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

    //     while (sqrRemainingDistance > float.Epsilon)
    //     {
    //         Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime); //inverseMoveTime * Time.deltaTime units closer to end
    //         rb2D.MovePosition(newPosition);
    //         sqrRemainingDistance = (transform.position - end).sqrMagnitude;
    //         yield return null;
    //     }
    // }

    public int GetLevel()
    {
        return level;
    }

    public float GetHealth()
    {
        return hp;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        hp = hp - amount;
    }

    public int GetArmor()
    {
        return ac;
    }
}
