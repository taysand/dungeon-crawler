using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{

    private static bool gameOver;
    private static GameOverMenu gameOverMenu;

    //adding buttons
    public GameObject mainMenuButtonPrefab;
    public GameObject restartButtonPrefab;
    private const string buttonsPath = "buttons panel";
    private Transform buttonsParent;

    protected override void AdditionalSetUp() {
        gameOver = false;
        gameOverMenu = GetComponent<GameOverMenu>();

        buttonsParent = transform.Find(buttonsPath);
    }

    protected override void BuildButtonsAndText() {
        BuildButton(mainMenu, buttonsParent, mainMenuButtonPrefab);
        BuildButton(restart, buttonsParent, restartButtonPrefab);
    }

    public static void ShowGameOver() {
        gameOver = true;
        gameOverMenu.ShowMenu();
    }

    //FIXME:delete this after testing
    public void ShowGameOverNotStatic() {
        ShowGameOver();
    }

    public static void HideGameOver() {
        gameOver = false;
		gameOverMenu.HideMenu();
    }

    public static bool IsGameOver() {
        return gameOver;
    }
}
