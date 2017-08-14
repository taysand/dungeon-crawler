using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelText : ChangingText {

	public override void UpdateTextField() {
		string currentLevel = "Level " + player.GetLevel() + "!";
		string levelProgress = "Level progress: " + player.GetExperience() + "/" + player.GetNextLevelXP();
		string availablePoints = "Available points: " + LevelUp.GetLevelUpPoints();
		textField.text = currentLevel + " " + levelProgress + " " + availablePoints;
	}
}
