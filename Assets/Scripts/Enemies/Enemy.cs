using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Moving {
    //coroutine stuff from https://unity3d.com/learn/tutorials/topics/scripting/coroutines?playlist=17117
    //turn based stuff from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial

    //stats
    protected float damagePerHit;
    protected float rangeRadius;
    protected int sleepTime;
    protected int freezeTime;
    protected int scaredTime;

    //conditions
    private bool sleeping = false;
    private bool frozen = false;
    private bool scared = false;
    private int turnsSpentSleeping = 0;
    private int turnsSpentFrozen = 0;
    private int turnsSpentScared = 0;
    private int turnsToSleep;
    private int turnsToFreeze;
    private int turnsToScare;

    Player player;

    //movement
    public Vector3 startingLocation;
    public Vector3 endLocation;
    private bool movingToPlayer;
    private int yDirection;
    private int xDirection;
    private bool lastMovedX;
    private bool movingToEnd;

    private bool facingRight;
    private bool lastRight;

    protected override void Start () {
        base.Start ();
        Game.AddEnemyToList (this);

        GetComponent<CircleCollider2D> ().radius = rangeRadius;

        GameObject playerGameObj = GameObject.Find (Game.playerTag);
        if (playerGameObj != null) {
            player = playerGameObj.GetComponent<Player> ();
        } else {
            Debug.Log ("no player object?");
        }

        transform.position = startingLocation;
        lastMovedX = false;
        movingToPlayer = false;
        movingToEnd = true;
        facingRight = spriteRenderer.flipX;
        lastRight = facingRight;
    }

    void Update () {
        if (lastRight != facingRight) {
            spriteRenderer.flipX = facingRight;
            lastRight = facingRight;
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == Game.playerTag) {
            if (!frozen && !sleeping && !scared) {
                Attack (other.gameObject.GetComponent<Player> ());
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ((other.gameObject.tag == Game.playerTag) && !movingToPlayer) {
            movingToPlayer = true;
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if ((other.gameObject.tag == Game.playerTag)) {
            movingToPlayer = false;
        }
    }

    void OnMouseDown () {
        if (Spell.Waiting ()) {
            Debug.Log ("clicked on " + gameObject);
            Spell.SetEnemy (this);
        }
    }

    public override void PlayAttackAnimation () {
        animator.SetTrigger (Moving.attackAnimation);
    }

    public void DecreaseLevel (int amount) {
        level = level - amount;
    }

    public void Sleep (int additionalSleepTurns) {
        sleeping = true;
        turnsToSleep = sleepTime + additionalSleepTurns;
    }

    public void WakeUp () {
        turnsSpentSleeping = 0;
        sleeping = false;
    }

    public void Freeze (int additionalFreezeTime) {
        frozen = true;
        turnsToFreeze = freezeTime + additionalFreezeTime;
    }

    public void Unfreeze () {
        turnsSpentFrozen = 0;
        frozen = false;
    }

    public void Scare (int additionalScareTime, int additionalScareDistance) {
        scared = true;
        turnsToScare = scaredTime + additionalScareTime;
    }

    public void NoLongerScared () {
        scared = false;
        turnsSpentScared = 0;
    }

    public void Attack (Player player) {
        PlayAttackAnimation ();
        if (!Game.IsPlayersTurn ()) {
            player.TakeDamage (damagePerHit);
        }
    }

    private void SetDirections () {
        if (scared) {
            DetailSetDirection (player.connectedJoint.transform.position, false);
            turnsSpentScared++;
            if (turnsSpentScared >= turnsToScare) {
                NoLongerScared ();
            }
        } else if (movingToPlayer) {
            DetailSetDirection (player.connectedJoint.transform.position, true);
        } else if (movingToEnd) {
            DetailSetDirection (endLocation, true);
        } else if (!movingToEnd) {
            DetailSetDirection (startingLocation, true);
        }
    }

    private void DetailSetDirection (Vector2 goal, bool movingTo) {
        int direction = 1;
        if (!movingTo) {
            direction = -1;
        }
        if (connectedJoint.position.x > goal.x) {
            xDirection = -1 * direction;
            if (direction == 1) {
                if (facingRight) {
                    lastRight = true;
                }
                facingRight = false;
            } else {
                if (!facingRight) {
                    lastRight = false;
                }
                facingRight = true;
            }
        } else {
            xDirection = direction;
            if (direction == 1) {
                if (!facingRight) {
                    lastRight = false;
                }
                facingRight = true;
            } else {
                if (facingRight) {
                    lastRight = true;
                }
                facingRight = false;
            }
        }

        if (connectedJoint.position.y > goal.y) {
            yDirection = -1 * direction;
        } else {
            yDirection = direction;
        }
    }

    private Vector2 MoveOne () {
        int x = 0;
        int y = 0;
        if (lastMovedX) {
            y = yDirection;
            lastMovedX = false;
        } else {
            x = xDirection;
            lastMovedX = true;
        }
        return new Vector2 (x, y);
    }

    public void MoveEnemy () {
        if (sleeping) {
            if (turnsSpentSleeping < turnsToSleep) {
                turnsSpentSleeping++;
                return;
            } else {
                WakeUp ();
            }

        }
        if (frozen) {
            if (turnsSpentFrozen < turnsToFreeze) {
                turnsSpentFrozen++;
                return;
            } else {
                Unfreeze ();
            }
        }

        Vector2 movement;
        SetDirections ();

        movement = MoveOne ();

        if (Math.Abs (connectedJoint.position.x - endLocation.x) < 1 && Math.Abs (connectedJoint.position.y - endLocation.y) < 1 && movingToEnd) {
            movingToEnd = false;
        } else if (Math.Abs (connectedJoint.position.x - startingLocation.x) < 1 && Math.Abs (connectedJoint.position.y - startingLocation.y) < 1 && !movingToEnd) {
            movingToEnd = true;
        }

        Move ((int) (movement.x), (int) (movement.y));
    }

    public void Teleport (int x, int y) {
        Move (x, y);
    }
}