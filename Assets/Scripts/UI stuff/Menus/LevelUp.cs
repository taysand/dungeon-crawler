using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{

    static GameObject gameOb;
    static bool activated;

    private static int levelUpPoints = 0;
    private GameObject[] levelUpButtons;

    void Awake()
    {
        gameOb = GetComponent<CanvasRenderer>().gameObject;
        levelUpButtons = GameObject.FindGameObjectsWithTag(SpellButton.levelUpButtonTag);
        HideLevelUpWindow();
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
        gameOb.GetComponent<LevelUp>().UpdateLevelUpOptions();
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

    public void SpendLevelUpPoints()
    {
        levelUpPoints--;
        UpdateLevelUpOptions();
    }

    public void UpdateLevelUpOptions()
    {
        DisplayLevel.UpdateDisplayedLevel();

        if (levelUpPoints > 0)
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                Text text = button.GetComponentInChildren<Text>();
                if (text != null)
                {
                    SpellButton spell = button.GetComponent<SpellButton>();
                    if (Player.SpellIsKnown(spell.GetSpellName()))
                    {
                        button.interactable = false;
                    }
                    else
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
}
