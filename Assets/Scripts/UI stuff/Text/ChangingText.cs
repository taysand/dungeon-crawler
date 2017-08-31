using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChangingText : MonoBehaviour {

	protected Player player;
	protected Text textField;

	void Awake () {
		SetUpPlayerAndText();
	}

	void Start() {
		UpdateTextField();
	}
	
	public abstract void UpdateTextField();

	protected void SetUpPlayerAndText() {
		textField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find(Game.playerTag);
		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
		} else {
			 Debug.Log("the player is gone");
		}
	}
}
