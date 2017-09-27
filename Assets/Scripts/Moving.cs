using System.Collections;
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
    protected float currentMaxHP;
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

    public float GetCurrentMaxHP()
    {
        return currentMaxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        hp = hp - amount;
    }

    public int GetArmor()
    {
        return ac;
    }

    //FIXME: attempting turn based below
    public float moveTime = .1f;
    public LayerMask blockingLayer;

    // private BoxCollider2D boxCollider;
    // private Rigidbody2D rb2D;
    private float inverseMoveTime;

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        //returning bool and also returning RaycastHit2D called hit

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        { //check if anything was hit
            StartCoroutine(SmoothMovement(end));//TODO: fix this
            transform.position = end;
            return true; //able to move
        }

        return false; //couldn't move
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime); //inverseMoveTime * Time.deltaTime units closer to end
            rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
        {
            return;
        }

        //if something was hit
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}
