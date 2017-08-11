using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorUpgrade : MonoBehaviour {

	static Text armorField;
	static private Player player;

	// Use this for initialization
	void Start () {
		armorField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find(Game.playerTag);
 		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
			UpdateArmorField();
 		} else {
			Debug.Log("no player object?");
		}
	}

	public static void UpdateArmorField() {
		armorField.text = "Armor: " + player.GetArmor();
	}
}
