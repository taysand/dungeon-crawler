using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{

    static GameObject gm;
    static bool activated;

    private static int levelUpPoints = 0;
    private GameObject[] levelUpButtons;

    void Awake()
    {
        gm = GetComponent<CanvasRenderer>().gameObject;
        levelUpButtons = GameObject.FindGameObjectsWithTag("LevelUpButton");
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
        gm.SetActive(activated);
		GameplayUI.HideGameplayUI();
    }

    public static void StaticHideLevelUpWindow()
    {
        activated = false;
        gm.SetActive(activated);
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
    }

    void Update()
    {
        if (levelUpPoints > 0)
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                Text text = button.GetComponentInChildren<Text>();
                if (text != null)
                {
                    string label = text.text;
                    if (Player.SpellIsKnown(label))
                    {
                        button.interactable = false;
                    }
                }
                else
                {
                    button.interactable = true;
                }
            }
        }

        DisplayLevel.UpdateDisplayedLevel();

        if (levelUpPoints <= 0)
        {
            foreach (GameObject b in levelUpButtons)
            {
                Button button = b.GetComponent<Button>();
                button.interactable = false;
            }
        }


    }
}
