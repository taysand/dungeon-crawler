using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgrade : MonoBehaviour {

	Text healthField;
	private Player player;

	// Use this for initialization
	void Start () {
		healthField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find("Player");
 		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
			UpdateHealthField();
 		} else {
			Debug.Log("no player object?");
		}
	}
	
	public void UpdateHealthField() {
		healthField.text = player.GetHealthString();
	}
}
