using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{

    public Menu levelUpMenu;
    private static PauseMenu pauseMenu;

    // static GameObject gm;
    // static bool activated;

    // Use this for initialization
    // void Start()
    // {
    //     gm = GetComponent<CanvasRenderer>().gameObject;
    //     HidePauseWindow();
    // }

    // public static void HidePauseWindow()
    // {
    //     activated = false;
    //     gm.SetActive(activated);
    //     GameplayUI.ShowGameplayUI();
    //     Game.Unpause();
    // }

    // public static void ShowPauseWindow()
    // {
    //     activated = true;
    //     gm.SetActive(activated);
    //     GameplayUI.HideGameplayUI();
    //     Game.Pause();
    // }

    // public void Resume()
    // {
    //     HideMenu();
    // }

    public override void Awake() {
        base.Awake();
        pauseMenu = GetComponent<PauseMenu>();
    }

    public void ShowLevelUp() {
        HideMenu();
        levelUpMenu.ShowMenu();
    }

    public void RestartLevel() {
        if (activated) //pause window is open
        {
            HideMenu();
        } else //game over window is open
        {
            GameOverMenu.HideGameOver();
        }
        //TODO:
        Debug.Log("restart level");
    }

    public void OpenSettings() {
        HideMenu();
        //TODO:
        Debug.Log("open settings");
    }

    public void GoToMainMenu() {
        if (activated) //pause window is open
        {
            HideMenu();
        } else //game over window is open
        {
            GameOverMenu.HideGameOver();
        }
        //TODO:
        Debug.Log("open main menu");
    }

    public static bool Activated() {
        return pauseMenu.activated;
    }
}
