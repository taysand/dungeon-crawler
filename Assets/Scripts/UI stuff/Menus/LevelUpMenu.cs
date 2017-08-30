using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : Menu
{

    // static GameObject gm;
    // static bool activated;

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
    public HealthUpgradeText healthUpgradeText;
    public DisplayHealthText hpDisplay;
    public ArmorUpgradeText armorUpgradeText;

    //upgrade buttons
    public GameObject plusButtonPrefab;
    private const string health = "health";
    private const string armor = "armor";
    private const string font = "Arial.ttf";
    private Color fontColor;
    private const string upgradeParent = "hp and ac upgrade";

    private static LevelUpMenu levelUpMenu;

    public override void Awake() {
        base.Awake();
        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        } else
        {
            Debug.Log("no player object?");
        }

        // gm = GetComponent<CanvasRenderer>().gameObject;
        // HideLevelUpWindow();

        levelUpMenu = GetComponent<LevelUpMenu>();
        

        fontColor = new Color(.57f, .08f, 1f, 1f);
        AddNonSpellUpgrades();
    }

    private void AddNonSpellUpgrades() {
        BuildText(health);
        BuildButton(health);
        BuildText(armor);
        BuildButton(armor);
    }

    private void BuildText(string textType) {
        GameObject o = new GameObject();
        Text text = o.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource(typeof(Font), font) as Font;
        text.color = fontColor;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;

        if (textType == health)
        {
            o.AddComponent<HealthUpgradeText>();
        } else if (textType == armor)
        {
            o.AddComponent<ArmorUpgradeText>();
        } else
        {
            Debug.Log("wrong text type");
        }
        o.transform.SetParent(transform.Find(upgradeParent), false);
    }

    private void BuildButton(string buttonType) {
        GameObject button = Instantiate(plusButtonPrefab) as GameObject;
        if (buttonType == health)
        {
            button.GetComponent<Button>().onClick.AddListener(IncreaseHealth);
        } else if (buttonType == armor)
        {
            button.GetComponent<Button>().onClick.AddListener(IncreaseArmor);
        } else
        {
            Debug.Log("wrong button type");
        }
        button.transform.SetParent(transform.Find(upgradeParent), false);
        Transform childText = button.transform.Find("Text");
        Destroy(childText.gameObject);
    }

    public static bool Activated() {
        return levelUpMenu.activated;
    }

    public override void ShowMenu() {
        base.ShowMenu();
        
        if (levelDisplayStatic == null) {
             levelDisplayStatic = levelDisplay;
        }
        UpdateLevelUpOptions();
    }

    // public static void StaticHideLevelUpWindow()
    // {
    //     activated = false;
    //     gm.SetActive(activated);
    //     Game.Unpause();
    //     GameplayUI.ShowGameplayUI();
    // }

    // //the non static method for buttons
    // public void HideLevelUpWindow() {
    //     HideMenu();
    // }

    //public void HideEverything() {
    //	LUImage.HideAnnouncement();
    //	HideLevelUpWindow();
    //}

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
