using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : Menu {
    //tracking levels
    private static int levelUpPoints = 0;
    private static GameObject[] levelUpButtons;

    //player stat increases
    private static Player player;
    private const float healthIncrease = 50;
    private const int armorIncrease = 5;

    //text fields
    public DisplayHealthText hpDisplay;

    //adding buttons and text fields 
    private const string healthUpgradePath = "LevelUp/upgrade options/hp and ac upgrade/hp upgrade";
    private const string armorUpgradePath = "LevelUp/upgrade options/hp and ac upgrade/ac upgrade";
    private const string donePath = "LevelUp/done button panel";
    private const string titlePath = "LevelUp/title panel";
    private Transform healthUpgradeParent;
    private Transform armorUpgradeParent;
    private Transform doneParent;
    private Transform titleParent;
    public GameObject plusButtonPrefab;
    public GameObject doneButtonPrefab;
    private static LevelUpMenu levelUpMenu;
    private static string spellButtonParentName = "level up spell buttons";
    private static string acButtonParentName = "ac upgrade";
    private static string hpButtonParentName = "hp upgrade";

    protected override void AdditionalSetUp () {
        healthUpgradeParent = transform.Find (healthUpgradePath);
        armorUpgradeParent = transform.Find (armorUpgradePath);
        doneParent = transform.Find (donePath);
        titleParent = transform.Find (titlePath);

        levelUpMenu = GetComponent<LevelUpMenu> ();

        GameObject playerGameObj = GameObject.Find (Game.playerTag);
        if (playerGameObj != null) {
            player = playerGameObj.GetComponent<Player> ();
        } else {
            Debug.Log ("no player object?");
        }
    }

    protected override void BuildButtonsAndText () {
        BuildText (levelInfo, titleParent);
        BuildText (health, healthUpgradeParent);
        BuildButton (health, healthUpgradeParent, plusButtonPrefab);
        BuildText (armor, armorUpgradeParent);
        BuildButton (armor, armorUpgradeParent, plusButtonPrefab);
        BuildButton (done, doneParent, doneButtonPrefab);
    }

    public override void ShowMenu () {
        base.ShowMenu ();
        UpdateLevelUpOptions ();
    }

    public static void GainLevelUpPoint () {
        levelUpPoints++;
    }

    public static int GetLevelUpPoints () {
        return levelUpPoints;
    }

    public static void SpendLevelUpPoints () {
        levelUpPoints--;
        UpdateLevelUpOptions ();
    }

    public static void UpdateLevelUpOptions () {
        levelDisplay.UpdateTextField ();
        healthUpgradeText.UpdateTextField ();
        armorUpgradeText.UpdateTextField ();

        levelUpButtons = GameObject.FindGameObjectsWithTag (SpellButtons.levelUpButtonTag);

        if (levelUpPoints > 0) {
            foreach (GameObject b in levelUpButtons) {
                Button button = b.GetComponent<Button> ();
                string parentName = b.transform.parent.name;

                if (parentName == spellButtonParentName) {
                    Spell currentSpell = button.GetComponent<Spell> ();
                    if (!Player.SpellIsKnown (currentSpell.GetSpellName ()) && currentSpell.GetRequiredLevel () <= player.GetLevel ()) {
                        button.interactable = true;
                    }
                } else if (parentName == acButtonParentName) {
                    if (player.GetArmor () >= Player.GetMaxAC ()) {
                        button.interactable = false;
                    } else {
                        button.interactable = true;
                    }
                } else if (parentName == hpButtonParentName) {
                    
                    if (player.GetCurrentMaxHP () >= Player.GetMaxMaxHP ()) {
                        button.interactable = false;
                    } else {
                        button.interactable = true;
                    }
                } else {
                    Debug.Log ("parent of upgrade buttons not found");
                }
            }
        } else {
            foreach (GameObject b in levelUpButtons) {
                Button button = b.GetComponent<Button> ();
                button.interactable = false;
            }
        }
    }

    public void IncreaseHealth () {
        player.IncreaseMaxHP (healthIncrease);
        healthUpgradeText.UpdateTextField ();
        SpendLevelUpPoints ();
        hpDisplay.UpdateTextField ();
    }

    public void IncreaseArmor () {
        player.IncreaseArmor (armorIncrease);
        armorUpgradeText.UpdateTextField ();
        SpendLevelUpPoints ();
    }
}