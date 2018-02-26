using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour {
    //spell stats
    protected string spellName;
    protected float healthLost;
    protected int maxLevelAffected;
    protected int level = 0;
    protected int requiredLevel;

    //spell name constants
    public const string delevelSpell = "Reduce Level";
    public const string drainSpell = "HP Drain";
    public const string freezeSpell = "Freeze";
    public const string scareSpell = "Scare";
    public const string teleportSpell = "Teleport";
    public const string transformSpell = "Transform";
    public const string sleepSpell = "Sleep";

    //coroutine names
    private const string targetEnemy = "TargetEnemy";
    private const string waitForClick = "WaitForClick";

    //casting stuff
    private static bool casting = false;
    private Enemy enemy;
    protected Player player;
    private static Spell activeSpell;
    private float cantCastReadTime = .5f;
    private float cantCastFade = .03f;

    private static List<string> allSpellNames = new List<string> ();

    void Awake () {
        InitializeStats ();
        GameObject playerGameObj = GameObject.Find (Game.playerTag);
        if (playerGameObj != null) {
            player = playerGameObj.GetComponent<Player> ();
        } else {
            Debug.Log ("no player object?");
        }
    }

    protected abstract void InitializeStats ();

    public static void BuildSpellNameList () {
        allSpellNames.Add (delevelSpell);
        allSpellNames.Add (drainSpell);
        allSpellNames.Add (freezeSpell);
        allSpellNames.Add (scareSpell);
        allSpellNames.Add (teleportSpell);
        allSpellNames.Add (transformSpell);
        allSpellNames.Add (sleepSpell);
    }

    public abstract bool Cast (Enemy enemy);

    public float GetHealthLost () {
        return healthLost;
    }

    public string GetSpellName () {
        return spellName;
    }

    public int GetRequiredLevel () {
        return requiredLevel;
    }

    protected virtual void LevelUpSpell () {
        level++;
        maxLevelAffected += 5;
    }

    public static List<string> GetAllSpellNames () {
        return allSpellNames;
    }

    public static bool Casting () {
        return casting;
    }

    public void OnCastingClick () {
        StartCoroutine (targetEnemy);
    }

    public void OnLevelUpClick (Button button) {
        LevelUpMenu.SpendLevelUpPoints ();
        Player.LearnSpell (spellName);
        SpellButtons.CheckIfKnown (button);

        int buttonIndex = allSpellNames.IndexOf (spellName);

        Button castingButton = SpellButtons.GetAllCastingButtons () [buttonIndex];
        SpellButtons.UpdateSpellSlots (castingButton);

    }

    public IEnumerator TargetEnemy () {
        casting = true; //TODO: do I need this
        Game.Pause (); //stops movement
        GameplayUI.ShowInstructions ();

        activeSpell = GetComponent<Spell> ();
        Debug.Log ("active spell is: " + activeSpell);

        bool success = false;

        while (true) {
            yield return StartCoroutine (waitForClick);

            success = Cast (enemy);

            if (success) {

                Message successMessage = GameObject.Find (Message.spellSuccessMessageName).GetComponent<Message> ();
                successMessage.ShowMessage (1f, .04f, .03f);

                float healthLost = GetHealthLost ();

                Debug.Log ("spell cast");

                //https://stackoverflow.com/questions/3561202/check-if-instance-of-a-type
                if (!(GetType () == typeof (DrainSpell))) {
                    GameObject playerGameObj = GameObject.Find (Game.playerTag);
                    if (playerGameObj != null) {
                        playerGameObj.GetComponent<Player> ().TakeDamage (healthLost);
                    } else {
                        Debug.Log ("no player object?");
                    }
                }
                break;
            } else {
                Message cantCastMessage = GameObject.Find (Message.cantCastMessageName).GetComponent<Message> ();
                cantCastMessage.ShowMessage (cantCastReadTime, cantCastFade, cantCastFade);
            }
        }
        End ();
    }

    protected IEnumerator WaitForClick () {
        SpellButtons.ActivateCastingButtons (false);

        //http://answers.unity3d.com/questions/904427/waiting-for-a-mouse-click-in-a-coroutine.html
        while (true) {
            if (Input.GetMouseButtonDown (0)) {
                //https://answers.unity.com/questions/1300276/how-do-you-raycasthit2d-certainly-layers.html
                int layerMask = (LayerMask.GetMask ("Enemies"));

                //https://forum.unity3d.com/threads/unity-2d-raycast-from-mouse-to-screen.211708/
                RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero, 1f, layerMask);
                Debug.Log ("mouse clicked and hit " + hit.collider);
                if (hit.collider != null) {
                    if (hit.transform.tag == Game.playerTag) {
                        Debug.Log ("hit player");
                    }
                    if (hit.transform.tag == Game.enemyTag) {
                        enemy = hit.transform.gameObject.GetComponent<Enemy> ();
                        Debug.Log ("hit " + enemy);
                        yield break;
                    }
                }
            }
            yield return null;
        }
    }

    private void End () {
        Game.Unpause ();
        casting = false;
        SpellButtons.ActivateCastingButtons (true);
        activeSpell = null;
        GameplayUI.HideInstructions ();
    }

    public static void StopCoroutines (Spell spell) {
        if (spell != null) {
            spell.StopCoroutine (waitForClick);
            spell.StopCoroutine (targetEnemy);
            spell.End ();
        } else {
            Debug.Log ("no active spell");
        }
    }

    public static Spell GetActiveSpell () {
        return activeSpell;
    }
}