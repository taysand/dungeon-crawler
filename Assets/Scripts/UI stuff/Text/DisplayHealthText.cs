using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHealthText : ChangingText {

	public override void UpdateTextField() {
		// Debug.Log("updating health display");
		if (!GameOverMenu.IsGameOver()) {
			textField.text = player.GetHealthString();
		} else {
			textField.text = "HP: 0/" + player.GetCurrentMaxHP();
		}
	}
}
