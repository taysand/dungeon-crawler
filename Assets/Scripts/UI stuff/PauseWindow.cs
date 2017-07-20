using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour {

	static GameObject gm;
	// Use this for initialization
	void Start () {
		 gm = GetComponent<CanvasRenderer>().gameObject;
		 HidePauseWindow();
	}
	
	public static void HidePauseWindow() {
		gm.SetActive(false);
		GameplayUI.ShowGameplayUI();
		Game.Unpause();
	}

	public static void ShowPauseWindow() {
		gm.SetActive(true);
		GameplayUI.HideGameplayUI();
		Game.Pause();
	}

	public void Resume() {
		HidePauseWindow();
	}

	public void ShowLevelUp() {
		HidePauseWindow();
		LevelUp.ShowLevelUpWindow();
	}

	public void RestartLevel() {
		HidePauseWindow();
		//TODO:
		Debug.Log("restart level");
	}

	public void OpenSettings() {
		HidePauseWindow();
		//TODO:
		Debug.Log("open settings");
	}
	
	public void QuitGame() {
		HidePauseWindow();//probably 
		//TODO:
		Debug.Log("game quit");
	}
}
