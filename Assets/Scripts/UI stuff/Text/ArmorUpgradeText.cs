using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUpgradeText : ChangingText {
	public override void UpdateTextField () {
		textField.text = "Armor: " + player.GetArmor () + "/" + player.GetCurrentMaxAC();
		if (player.GetArmor () >= Player.GetMaxAC()) {
			textField.text = textField.text + "\nMax Armor!";
		}
	}
}