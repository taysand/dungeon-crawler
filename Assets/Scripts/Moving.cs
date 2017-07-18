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

    // Use this for initialization
    protected void Start()
    {
        SetStartingValues();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected abstract void SetStartingValues();

    protected abstract void Move();

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void PlayAttackAnimation()
    {
        //tells the animation reference to play the attack animation 
    }

    public void PlayMoveAnimation()
    {
        //tells the animation reference to play the move animation
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
