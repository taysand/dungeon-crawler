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
    private static bool waiting = false;
    private static Enemy enemy;
    protected Player player;
    private static Spell activeSpell;

    private static List<string> allSpellNames = new List<string> ();

    //messages
    private string spellSuccessText = "Success!";
    private float successMessageReadTime = 1f;
    private float successMessageFadeRate = .04f;
    private float successMessageFadeDelay = .03f;
    private float cantCastReadTime = 1f;
    private float cantCastFade = .03f;
    private string cantCastText = "Enemy is too powerful";

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
        casting = true;
        Game.Pause (); //stops movement
        GameplayUI.ShowInstructions ();

        activeSpell = GetComponent<Spell> ();
        Debug.Log ("active spell is: " + activeSpell);

        bool success = false;

        waiting = true;
        Debug.Log ("about to wait");
        yield return StartCoroutine (waitForClick);
        Debug.Log ("done waiting");
        success = Cast (enemy);

        if (success) {

            Message.SetAndDisplayMessage (successMessageReadTime, successMessageFadeRate, successMessageFadeDelay, spellSuccessText);

            float healthLost = GetHealthLost ();

            //https://stackoverflow.com/questions/3561202/check-if-instance-of-a-type
            if (!(GetType () == typeof (DrainSpell))) {
                GameObject playerGameObj = GameObject.Find (Game.playerTag);
                if (playerGameObj != null) {
                    playerGameObj.GetComponent<Player> ().TakeDamage (healthLost);
                } else {
                    Debug.Log ("no player object?");
                }
            }
        } else {
            Message.SetAndDisplayMessage (cantCastReadTime, cantCastFade, cantCastFade, cantCastText);
        }
        End ();
    }

    protected IEnumerator WaitForClick () {
        SpellButtons.ActivateCastingButtons (false);

        while (waiting) {
            yield return null;
        }
    }

    public static void SetEnemy (Enemy e) {
        enemy = e;
        waiting = false;
        Debug.Log ("set enemy to " + enemy);
    }

    public static bool Waiting () {
        return waiting;
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