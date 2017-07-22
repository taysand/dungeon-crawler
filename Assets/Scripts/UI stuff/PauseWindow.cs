using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{

    static GameObject gm;
    static bool activated;

    // Use this for initialization
    void Start()
    {
        gm = GetComponent<CanvasRenderer>().gameObject;
        HidePauseWindow();
    }

    public static void HidePauseWindow()
    {
        activated = false;
        gm.SetActive(activated);
        GameplayUI.ShowGameplayUI();
        Game.Unpause();
    }

    public static void ShowPauseWindow()
    {
        activated = true;
        gm.SetActive(activated);
        GameplayUI.HideGameplayUI();
        Game.Pause();
    }

    public void Resume()
    {
        HidePauseWindow();
    }

    public void ShowLevelUp()
    {
        HidePauseWindow();
        LevelUp.ShowLevelUpWindow();
    }

    public void RestartLevel()
    {
        if (activated) //pause window is open
        { 
            HidePauseWindow();
        }
        else //game over window is open
        {
			GameOver.HideGameOver();
        }
        //TODO:
        Debug.Log("restart level");
    }

    public void OpenSettings()
    {
        HidePauseWindow();
        //TODO:
        Debug.Log("open settings");
    }

    public void GoToMainMenu()
    {
        if (activated) //pause window is open
        { 
            HidePauseWindow();
        }
        else //game over window is open
        {
			GameOver.HideGameOver();
        }
                          //TODO:
        Debug.Log("open main menu");
    }

    public static bool Activated()
    {
        return activated;
    }
}
