using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgradeText : ChangingText {

	public override void UpdateTextField() {
		textField.text = player.GetHealthString();
		if (player.GetCurrentMaxHP() >= Player.maxMaxHP) {
			textField.text = textField.text + "\nMax HP!";
		}
	}
}
