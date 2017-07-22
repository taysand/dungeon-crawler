using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	static GameObject gm;

	// Use this for initialization
	void Awake () {
		gm = GetComponent<Canvas>().gameObject;
		HideGameOver();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ShowGameOver() {
		gm.SetActive(true);
		//DisplayPlayerHealth.UpdateHealthDisplay();
		Game.Pause();
	}

	//delete this after testing
	public void ShowGameOverNotStatic() {
		ShowGameOver();
	}

	public static void HideGameOver() {
		gm.SetActive(false);
		Game.Unpause();
	}
}
