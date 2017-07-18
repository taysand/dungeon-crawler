using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorUpgrade : MonoBehaviour {

	Text armorField;
	private Player player;

	// Use this for initialization
	void Start () {
		armorField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find("Player");
 		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
			UpdateArmorField();
 		} else {
			Debug.Log("no player object?");
		}
	}

	public void UpdateArmorField() {
		armorField.text = "Armor: " + player.GetArmor();
	}
}
