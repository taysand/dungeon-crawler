using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelText : ChangingText {

	public override void UpdateTextField() {
		string currentLevel = "Level " + player.GetLevel() + "!";
		string levelProgress = "Level progress: " + player.GetExperience() + "/" + player.GetNextLevelXP();
		if (player.GetLevel() >= player.GetMaxLevel()) {
			levelProgress = levelProgress + " Max level!";
		}
		string availablePoints = "Available points: " + LevelUpMenu.GetLevelUpPoints();
		textField.text = currentLevel + " " + levelProgress + " " + availablePoints;
	}
}
