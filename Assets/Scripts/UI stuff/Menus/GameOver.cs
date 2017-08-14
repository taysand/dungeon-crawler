using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	static GameObject gm;
	private static bool gameOver;

	void Awake () {
		gm = GetComponent<Canvas>().gameObject;
		HideGameOver();
	}

	public static void ShowGameOver() {
		gameOver = true;
		gm.SetActive(true);
		//DisplayPlayerHealth.UpdateHealthDisplay();
		Game.Pause();
	}

	//delete this after testing
	public void ShowGameOverNotStatic() {
		ShowGameOver();
	}

	public static void HideGameOver() {
		gameOver = false;
		gm.SetActive(false);
		Game.Unpause();
	}

	public static bool IsGameOver() {
		return gameOver;
	}
}
