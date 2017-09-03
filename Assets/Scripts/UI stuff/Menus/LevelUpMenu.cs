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

    //text fields
    public DisplayHealthText hpDisplay;

    //adding buttons and text fields 
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
                string parentName = b.transform.parent.name;

                if (parentName == "level up spell buttons")
                {
                    //TODO:constant that string
                    if (!Player.SpellIsKnown(button.GetComponent<Spell>().GetSpellName()))
                    {
                        button.interactable = true;
                    }
                } else if (parentName == "hp and ac upgrade") {//(parentName == "ac upgrade")
                //{
                    // if (player.GetArmor() >= Player.maxAC)
                    // {
                    //     Debug.Log("player armor is " + player.GetArmor() + " and max armor is " + Player.maxAC);
                    //     //armor button is off
                    //     //display some sort of message when this happens
                    // } else
                    // {
                        button.interactable = true;
                //     }
                // } else if (parentName == "hp upgrade")
                // {
                //     if (player.GetCurrentMaxHP() >= Player.maxMaxHP)
                //     {
                //         Debug.Log("player health is " + player.GetCurrentMaxHP() + " and max health is " + Player.maxMaxHP);
                //         //health button is off
                //         //display some sort of message when this happens
                //     } else
                //     {
                //         button.interactable = true;
                //     }
                } else
                {
                    Debug.Log("parent of upgrade buttons not found");
                }





                // Text text = button.GetComponentInChildren<Text>();
                // if (text != null){
                //     //a spell button. TODO: this is a very bad way to do this. probably check what their parent is?
                // //if the parent is "level up spell buttons" then it's a spell button

                // //else if parent is "ac upgrade" it's the armor one
                // //else if parent is "hp upgrade" it's the health one


                // } else//plus buttons (armor and health) are named "plus button(Clone)"
                // {
                //     //TODO: some sort of limits on max hp and ac



                //     }
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
