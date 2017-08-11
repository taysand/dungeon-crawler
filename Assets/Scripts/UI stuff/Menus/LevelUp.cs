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
    private Player player;
    private const float healthIncrease = 50;
    private const int armorIncrease = 5;

    void Awake()
    {
        gameOb = GetComponent<CanvasRenderer>().gameObject;
        HideLevelUpWindow();
    }

    void Start()
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
        DisplayLevel.UpdateDisplayedLevel();

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
        HealthUpgrade.UpdateHealthField();
        SpendLevelUpPoints();
        DisplayPlayerHealth.UpdateHealthDisplay();
    }

    public void IncreaseArmor()
    {
        player.IncreaseArmor(armorIncrease);
        ArmorUpgrade.UpdateArmorField();
        SpendLevelUpPoints();
    }
}
