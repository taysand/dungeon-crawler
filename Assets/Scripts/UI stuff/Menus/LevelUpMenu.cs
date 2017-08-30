using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : Menu
{
    //tracking levels
    private static int levelUpPoints = 0;
    private static GameObject[] levelUpButtons;

    //player stat increases
    private static Player player;
    private const float healthIncrease = 50;
    private const int armorIncrease = 5;
    private const float maxHealth = 1000f;
    private const int maxArmor = 20;

    //text fields
    public DisplayHealthText hpDisplay;

    //adding buttons and text fields 
    private const string levelUpFont = "Arial.ttf";
    private const string upgradePath = "LevelUp/upgrade options/hp and ac upgrade";
    private const string donePath = "LevelUp/done button panel";
    private const string titlePath = "LevelUp/title panel";
    private Transform upgradeParent;
    private Transform doneParent;
    private Transform titleParent;
    public GameObject plusButtonPrefab;
    public GameObject doneButtonPrefab;
    private static LevelUpMenu levelUpMenu;

    protected override void AdditionalSetUp() {
        font = levelUpFont;
        fontColor = new Color(.57f, .08f, 1f, 1f);

        upgradeParent = transform.Find(upgradePath);
        doneParent = transform.Find(donePath);
        titleParent = transform.Find(titlePath);
        
        levelUpMenu = GetComponent<LevelUpMenu>();

        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        } else
        {
            Debug.Log("no player object?");
        }
    }

    protected override void BuildButtonsAndText() {
        BuildText(levelInfo, titleParent);
        BuildText(health, upgradeParent);
        BuildButton(health, upgradeParent, plusButtonPrefab);
        BuildText(armor, upgradeParent);
        BuildButton(armor, upgradeParent, plusButtonPrefab);
        BuildButton(done, doneParent, doneButtonPrefab);
    }

    public override void ShowMenu() {
        base.ShowMenu();
        UpdateLevelUpOptions();
    }

    public static void GainLevelUpPoint() {
        levelUpPoints++;
    }

    public static int GetLevelUpPoints() {
        return levelUpPoints;
    }

    public static void SpendLevelUpPoints() {
        levelUpPoints--;
        UpdateLevelUpOptions();
    }

    public static void UpdateLevelUpOptions() {
        levelDisplay.UpdateTextField();

        levelUpButtons = GameObject.FindGameObjectsWithTag(SpellButtons.levelUpButtonTag);

        foreach (GameObject b in levelUpButtons)
        {
            Debug.Log(b);
        }

        if (levelUpPoints > 0)
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                Text text = button.GetComponentInChildren<Text>();
                if (text != null)//a spell button
                {
                    if (!Player.SpellIsKnown(button.GetComponent<Spell>().GetSpellName()))
                    {
                        button.interactable = true;
                    }
                } else
                {
                    //TODO: some sort of limits on max hp and ac
                    if (player.GetArmor() < maxArmor)
                    {
                        //armor button is on
                    }
                    if (player.GetMaxHP() < maxHealth)
                    {
                        //health button is on
                    }
                    button.interactable = true;
                }
            }
        } else
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }

    public void IncreaseHealth() {
        player.IncreaseMaxHP(healthIncrease);
        healthUpgradeText.UpdateTextField();
        SpendLevelUpPoints();
        hpDisplay.UpdateTextField();
    }

    public void IncreaseArmor() {
        player.IncreaseArmor(armorIncrease);
        armorUpgradeText.UpdateTextField();
        SpendLevelUpPoints();
    }
}
