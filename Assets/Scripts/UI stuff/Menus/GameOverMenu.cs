using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{
    private static bool gameOver;
    private static GameOverMenu gameOverMenu;

    //adding buttons
    public GameObject buttonPrefab;
    private const string buttonsPath = "buttons panel";
    private Transform buttonsParent;

    protected override void AdditionalSetUp() {
        gameOver = false;
        gameOverMenu = GetComponent<GameOverMenu>();

        buttonsParent = transform.Find(buttonsPath);
    }

    protected override void BuildButtonsAndText() {
        // BuildButton(mainMenu, buttonsParent, buttonPrefab);
        BuildButton(restart, buttonsParent, buttonPrefab);
    }

    public static void ShowGameOver() {
        gameOver = true;
        gameOverMenu.ShowMenu();
    }

    //for testing
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
