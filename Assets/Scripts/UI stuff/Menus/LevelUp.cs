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

        if (levelUpPoints > 0)//all buttons should be available, unless they're already learned
        {
            Debug.Log("points available");
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                Text text = button.GetComponentInChildren<Text>();
                if (text != null)//if there is text aka if it's a spell button
                {
                    Debug.Log("setting spell buttons");
                    string label = text.text;
                    if (Player.SpellIsKnown(label))//if we know the spell it shouldn't be an option 
                    {
                        Debug.Log(label + " is known");
                        button.interactable = false;
                    }
                }
                else//if it's not a spell button then it's an option
                {
                    Debug.Log("setting other buttons");
                    button.interactable = true;
                }
            }
        }
        else//no level up points, nothing should be active
        {
            Debug.Log("no points");
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                button.interactable = false;
            }
        }
    }
}
