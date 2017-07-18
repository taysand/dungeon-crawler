using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel : MonoBehaviour {

	static Text text;
	static Player player;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		GameObject playerGameObj = GameObject.Find("Player");
		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
		} else {
			 Debug.Log("the player is gone");
		}
		UpdateDisplayedLevel();
	}
	
	public static void UpdateDisplayedLevel() {
		string currentLevel = "Level " + player.GetLevel() + "!";
		string levelProgress = "Level progress: " + player.GetExperience() + "/" + player.GetNextLevelXP();
		string availablePoints = "Available points: " + LevelUp.GetLevelUpPoints();
		text.text = currentLevel + " " + levelProgress + " " + availablePoints;
	}
}
