using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : Menu
{

    // static GameObject gm;
    private static bool gameOver;
    private static GameOverMenu gameOverMenu;

    // void Awake () {
    // 	gm = GetComponent<Canvas>().gameObject;
    // 	HideGameOver();
    // }

    public override void Awake() {
        base.Awake();
		gameOver = false;
        gameOverMenu = GetComponent<GameOverMenu>();
    }

    public static void ShowGameOver() {
        gameOver = true;
        gameOverMenu.ShowMenu();
    }

    //delete this after testing
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
