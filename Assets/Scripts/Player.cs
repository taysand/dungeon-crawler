﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//images 
//https://pixabay.com/en/chapeau-hat-magic-1293080/
//https://www.metmuseum.org/art/collection/search/169172
//https://www.shutterstock.com/image-vector/group-working-people-diversity-diverse-business-588269213
public class Player : Moving {
    //stats
    private float playerStartingHP = 100f;
    private int playerStartingAC = 2;
    private float playerStartingMaxHP = 100f;
    private float playerStartingSpeed = 2.7f;

    //leveling
    private int xp = 0;
    private int[] levels = { 100, 300, 600, 1000, 1500, 2100, 2800 };
    private int nextLevel;
    private int maxLevel;
    private const int maxAC = 27;
    private const float maxMaxHP = 1000f;

    //conditions
    private bool hidden = false;

    //magic stuff
    private static List<string> knownSpells = new List<string> ();

    //menu things
    public DisplayHealthText hpDisplay;
    public Menu levelUpMenu;
    public DisplayFriendsText friendsDisplay;

    //friendship
    private int numFriends = 0;
    private const string friendTag = "friend";

    //messages
    private float delayAfterLevelUpMessage = .8f;
    private float levelMessageReadTime = 1.8f;
    private float levelMessageFadeRate = .04f;
    private float levelMessageFadeDelay = .03f;
    private string levelMessageText = "Level up!";
    private float friendMessageReadTime = 3.6f;
    private float friendMessageFadeRate = .04f;
    private float friendMessageFadeDelay = .03f;
    private string friendMessageText = "You got a friend!";

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

        horizontal = (int) (Input.GetAxisRaw (Game.horizontalString));
        vertical = (int) (Input.GetAxisRaw (Game.verticalString));
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
            Message.SetAndDisplayMessage (friendMessageReadTime, friendMessageFadeRate, friendMessageFadeDelay, friendMessageText);
            other.gameObject.SetActive (false);
        }
    }

    protected override void SetStartingValues () {
        hp = playerStartingHP;
        ac = playerStartingAC;
        level = 0;
        currentMaxHP = playerStartingMaxHP;
        speed = playerStartingSpeed;
        currentMaxAC = ac;
    }

    public override void PlayAttackAnimation () { }

    public void PlayInjuredAnimation () {
        animator.SetTrigger (Moving.playerInjuredAnimation);
    }

    public override void TakeDamage (float amount) {
        Debug.Log("damage maybe without ac");
        base.TakeDamage (amount);
        hpDisplay.UpdateTextField ();
        PlayInjuredAnimation ();
        if (hp <= 0) {
            GameOverMenu.ShowGameOver ();
        }
    }

    public void TakeDamageWithAC (float amount) {
        Debug.Log("damage with ac");
        float totalDamage = amount - ac;
        if (totalDamage < 0) {
            totalDamage = 0;
        }
        ac--;
        if (ac < 0) {
            ac = 0;
        }
        TakeDamage(amount);
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
            ac = currentMaxAC;
            StartCoroutine (ShowLevelUpStuff ());
        }
    }

    private IEnumerator ShowLevelUpStuff () {
        Message.SetAndDisplayMessage (levelMessageReadTime, levelMessageFadeRate, levelMessageFadeDelay, levelMessageText);
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

    public int GetNextLevel() {
        return nextLevel;
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
        currentMaxAC = currentMaxAC + amount;
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

    public void SkipTurn () {
        Game.SetPlayersTurn (false);
    }

    public static int GetMaxAC () {
        return maxAC;
    }

    public static float GetMaxMaxHP () {
        return maxMaxHP;
    }
}