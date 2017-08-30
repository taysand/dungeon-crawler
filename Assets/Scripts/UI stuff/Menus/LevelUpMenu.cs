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
    private static DisplayLevelText levelDisplayStatic;
    public DisplayLevelText levelDisplay;
    public DisplayHealthText hpDisplay;

    //adding buttons and text fields 
    private const string levelUpFont = "Arial.ttf";
    private const string levelUpUpgradeParent = "LevelUp/hp and ac upgrade";

    private static LevelUpMenu levelUpMenu;

    protected override void AdditionalSetUp() {
        font = levelUpFont;
        fontColor = new Color(.57f, .08f, 1f, 1f);
        Debug.Log("upgrade parent is being set to what it should be");
        upgradeParent = levelUpUpgradeParent;

        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        } else
        {
            Debug.Log("no player object?");
        }

        levelUpMenu = GetComponent<LevelUpMenu>();

        if (levelDisplayStatic == null)
        { 
            levelDisplayStatic = levelDisplay;
        }
    }

    protected override void BuildButtonsAndText() {
        BuildText(health);
        BuildButton(health);
        BuildText(armor);
        BuildButton(armor);
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
        levelDisplayStatic.UpdateTextField();

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
