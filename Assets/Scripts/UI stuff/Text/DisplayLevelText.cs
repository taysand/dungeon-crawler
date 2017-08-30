using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelText : ChangingText {

	public override void UpdateTextField() {
		if ((textField == null) || (player == null)) {
			SetUpPlayerAndText();
		}
		string currentLevel = "Level " + player.GetLevel() + "!";
		string levelProgress = "Level progress: " + player.GetExperience() + "/" + player.GetNextLevelXP();
		string availablePoints = "Available points: " + LevelUpMenu.GetLevelUpPoints();
		textField.text = currentLevel + " " + levelProgress + " " + availablePoints;
	}
}
