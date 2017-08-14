using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ChangingText : MonoBehaviour {

	protected Player player;
	protected Text textField;

	// Use this for initialization
	void Awake () {
		textField = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find(Game.playerTag);
		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
		} else {
			 Debug.Log("the player is gone");
		}
	}

	void Start() {
		UpdateTextField();
	}
	
	public abstract void UpdateTextField();
}
