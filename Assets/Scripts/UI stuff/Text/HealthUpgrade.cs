using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrade : MonoBehaviour {

	static Text healthField;
	static private Player player;

	// Use this for initialization
	void Start () {
		healthField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find(Game.playerTag);
 		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
			UpdateHealthField();
 		} else {
			Debug.Log("no player object?");
		}
	}
	
	public static void UpdateHealthField() {
		healthField.text = player.GetHealthString();
	}
}
