using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{

    public Menu levelUpMenu;
    private static PauseMenu pauseMenu;
    private const string path = "Pause";
    private Transform parent;
    public GameObject buttonPrefab;

    protected override void AdditionalSetUp() {
        pauseMenu = GetComponent<PauseMenu>();

        parent = transform.Find(path);
    }

    protected override void BuildButtonsAndText() {
        BuildText(pauseTitle, parent);
        BuildButton(resume, parent, buttonPrefab);
        BuildButton(levelUp, parent, buttonPrefab);
        BuildButton(restart, parent, buttonPrefab);
        BuildButton(settings, parent, buttonPrefab);
        BuildButton(mainMenu, parent, buttonPrefab);
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
        Debug.Log("restarting level");
    }

    public void OpenSettings() {
        HideMenu();
        //TODO:
        Debug.Log("opening settings");
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
        Debug.Log("opening main menu");
    }
}
