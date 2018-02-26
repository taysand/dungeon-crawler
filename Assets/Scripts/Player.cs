using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving {
    //stats
    public float playerStartingHP = 100f; //should be constant
    public int playerStartingAC = 2; //should be constant
    public float playerStartingMaxHP = 100f; //should be constant
    public float playerStartingSpeed = 10f; //should be constant

    //leveling
    private int xp = 0;
    private int[] levels = { 100, 300, 600, 1000, 1500, 2100, 2800 };
    private int nextLevel;
    private int maxLevel;
    private float delayAfterLevelUpMessage = .8f;
    private float levelMessageReadtime = .01f;
    private float levelMessageFadeRate = .04f;
    private float levelMessageFadeDelay = .03f;
    public const int maxAC = 27;
    public const float maxMaxHP = 1000f;

    //conditions
    private bool hidden = false;

    //magic stuff
    private static List<string> knownSpells = new List<string> ();

    //menu things
    public DisplayHealthText hpDisplay;
    public Menu levelUpMenu;
    public DisplayFriendsText friendsDisplay;

    //friendship
    public int numFriends = 0;
    private const string friendTag = "friend";

    protected override void Start () {
        base.Start ();
        nextLevel = levels[level];
        maxLevel = levels.Length;
    }

    void Update () {
        if (!Game.IsPlayersTurn ()) {
            return;
        }

        int horizontal = 0;
        int vertical = 0;

        horizontal = 2 * (int) (Input.GetAxisRaw (Game.horizontalString));
        vertical = 2 * (int) (Input.GetAxisRaw (Game.verticalString));
        if (horizontal != 0) {
            vertical = 0;
        }

        if (horizontal != 0 || vertical != 0) {
            Move (horizontal, vertical);
            Game.SetPlayersTurn (false);
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ((other.gameObject.tag == friendTag)) {
            numFriends++;
            friendsDisplay.UpdateTextField ();
            Message gotFriendMessage = GameObject.Find (Message.gotFriendMessageName).GetComponent<Message> ();
            gotFriendMessage.ShowMessage (2.6f, .04f, .03f);
            other.gameObject.SetActive (false);
        }
    }

    protected override void SetStartingValues () {
        hp = playerStartingHP;
        ac = playerStartingAC;
        level = 0;
        currentMaxHP = playerStartingMaxHP;
        speed = playerStartingSpeed;
    }

    public override void PlayAttackAnimation () { }

    public void PlayInjuredAnimation () {
        animator.SetTrigger (Moving.playerInjuredAnimation);
    }

    public override void TakeDamage (float amount) {
        base.TakeDamage (amount);
        hpDisplay.UpdateTextField ();
        PlayInjuredAnimation ();
        if (hp <= 0) {
            GameOverMenu.ShowGameOver ();
        }
    }

    public void UsePotion () { }

    public void Heal (float amount) {
        if ((amount + hp) >= currentMaxHP) {
            hp = currentMaxHP;
        } else {
            hp = hp + amount;
        }
        hpDisplay.UpdateTextField ();
    }

    public void IncreaseMaxHP (float amount) {
        currentMaxHP = amount + currentMaxHP;
        hp = amount + hp;
        hpDisplay.UpdateTextField ();
    }

    public void LevelUp () {
        if (level < maxLevel) {
            level++;

            if (level < maxLevel) {
                nextLevel = levels[level];
            }

            LevelUpMenu.GainLevelUpPoint ();
            StartCoroutine (ShowLevelUpStuff ());
        }
    }

    private IEnumerator ShowLevelUpStuff () {
        Message levelUpMessage = GameObject.Find (Message.levelUpMessageName).GetComponent<Message> ();
        levelUpMessage.ShowMessage (levelMessageReadtime, levelMessageFadeRate, levelMessageFadeDelay);
        yield return new WaitForSeconds (delayAfterLevelUpMessage);
        levelUpMenu.ShowMenu ();
    }

    public int GetExperience () {
        return xp;
    }

    public void IncreaseXP (int amount) {
        xp = xp + amount;
        if (xp >= nextLevel) {
            LevelUp ();
        }
    }

    public void Hide () {
        hidden = true;
    }

    public void NotHide () {
        hidden = false;
    }

    public bool Hidden () {
        return hidden;
    }

    public void IncreaseArmor (int amount) {
        ac = ac + amount;
    }

    public static List<string> GetKnownSpells () {
        return knownSpells;
    }

    public static void LearnSpell (string spell) {
        knownSpells.Add (spell);
    }

    public static void ResetSpellList () {
        knownSpells = new List<string> ();
    }

    public string GetHealthString () {
        return "HP: " + hp + "/" + currentMaxHP;
    }

    public int GetNextLevelXP () {
        return nextLevel;
    }

    public static bool SpellIsKnown (string spellName) {
        return GetKnownSpells ().Contains (spellName);
    }

    //to test leveling up. there's a button which should also be deleted
    public void TempLevelUpButton () {
        IncreaseXP (nextLevel - xp);
    }

    public int GetMaxLevel () {
        return maxLevel;
    }

    public int GetNumFriends () {
        return numFriends;
    }

    public void SkipTurn() {
        Game.SetPlayersTurn (false);
    }
}