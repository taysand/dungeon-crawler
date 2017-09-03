using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUpgradeText : ChangingText {

	public override void UpdateTextField() {
		textField.text = "AC: " + player.GetArmor();
		if (player.GetArmor() >= Player.maxAC) {
			textField.text = textField.text + "\nMax AC!";
		}
	}
}
