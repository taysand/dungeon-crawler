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

    protected void Start()
    {
        SetStartingValues();
        animator = GetComponent<Animator>();
    }

    protected abstract void SetStartingValues();

    public abstract void PlayAttackAnimation();

    protected void Move() {
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
