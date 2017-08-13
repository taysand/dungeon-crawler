using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{

    static GameObject gameOb;
    static bool activated;

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
    [SerializeField] private DisplayLevelText levelDisplay;
    [SerializeField] private HealthUpgradeText healthUpgradeText;
    [SerializeField] private DisplayHealthText hpDisplay;
    [SerializeField] private ArmorUpgradeText armorUpgradeText;

    void Awake()
    {
        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        } 

        gameOb = GetComponent<CanvasRenderer>().gameObject;
        HideLevelUpWindow();
        levelDisplayStatic = levelDisplay;
    }

    public static bool Activated()
    {
        return activated;
    }

    public static void ShowLevelUpWindow()
    {
        Game.Pause();
        activated = true;
        gameOb.SetActive(activated);
        GameplayUI.HideGameplayUI();
        UpdateLevelUpOptions();
    }

    public static void StaticHideLevelUpWindow()
    {
        activated = false;
        gameOb.SetActive(activated);
        Game.Unpause();
        GameplayUI.ShowGameplayUI();
    }

    public void HideLevelUpWindow()
    {
        StaticHideLevelUpWindow();
    }

    //public void HideEverything() {
    //	LUImage.HideAnnouncement();
    //	HideLevelUpWindow();
    //}

    public static void GainLevelUpPoint()
    {
        levelUpPoints++;
    }

    public static int GetLevelUpPoints()
    {
        return levelUpPoints;
    }

    public static void SpendLevelUpPoints()
    {
        levelUpPoints--;
        UpdateLevelUpOptions();
    }

    public static void UpdateLevelUpOptions()
    {
        levelDisplayStatic.UpdateTextField();

        levelUpButtons = GameObject.FindGameObjectsWithTag(SpellButtons.levelUpButtonTag);
        
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
                }
                else
                {
                    //TODO: some sort of limits on max hp and ac
                    if (player.GetArmor() < maxArmor) {
                        //armor button is on
                    }
                    if (player.GetMaxHP() < maxHealth) {
                        //health button is on
                    }
                    button.interactable = true;
                }
            }
        }
        else
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }

    public void IncreaseHealth()
    {
        player.IncreaseMaxHP(healthIncrease);
        healthUpgradeText.UpdateTextField();
        SpendLevelUpPoints();
        hpDisplay.UpdateTextField();
    }

    public void IncreaseArmor()
    {
        player.IncreaseArmor(armorIncrease);
        armorUpgradeText.UpdateTextField();
        SpendLevelUpPoints();
    }
}
