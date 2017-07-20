using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moving : MonoBehaviour
{

    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    protected bool facingRight = true;

    //stats
    protected int level;
    protected float hp;
    protected int ac;
    protected float maxHP;

    public float moveTime = 0.1f;

    protected void Start()
    {
        SetStartingValues();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected abstract void SetStartingValues();

    public abstract void PlayAttackAnimation();

    protected void Flip() //do I need this
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Move(int x, int y) {
        Vector2 start = transform.position;
		Vector2 end = start + new Vector2(x, y);
        transform.position = end;
	}

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

    public void TakeDamage(float amount)
    {
        float damage = amount - ac;
        hp = hp - damage;
    }

    public int GetArmor()
    {
        return ac;
    }
}
