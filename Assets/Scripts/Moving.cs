using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stop sprite rotation https://answers.unity.com/questions/671233/unity-2d-rigidbody-rotation-constraints.html
public abstract class Moving : MonoBehaviour {
    public const string playerInjuredAnimation = "PlayerInjured";
    public const string attackAnimation = "Attack";

    protected Animator animator;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    protected SpriteRenderer spriteRenderer;
    public float moveTime = .1f;
    public Transform connectedJoint;

    //stats
    protected int level;
    protected float hp;
    protected int ac;
    protected int currentMaxAC;
    protected float currentMaxHP;
    protected float speed;

    protected virtual void Start () {
        SetStartingValues ();
        animator = GetComponent<Animator> ();
        boxCollider = GetComponent<BoxCollider2D> ();
        rb2D = GetComponent<Rigidbody2D> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        rb2D.drag = 5;
    }

    protected abstract void SetStartingValues ();

    public abstract void PlayAttackAnimation ();

    public int GetLevel () {
        return level;
    }

    public float GetHealth () {
        return hp;
    }

    public float GetCurrentMaxHP () {
        return currentMaxHP;
    }

    public virtual void TakeDamage (float amount) {
        hp = hp - amount;
    }

    public int GetArmor () {
        return ac;
    }

    public int GetCurrentMaxAC() {
        return currentMaxAC;
    }

    protected void Move (int xDir, int yDir) {
        Vector2 start = connectedJoint.position;
        Vector2 end = start + new Vector2 (speed * xDir, speed * yDir);
        connectedJoint.position = end;
    }
}