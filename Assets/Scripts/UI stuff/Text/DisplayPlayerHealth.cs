﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour {

	static Text healthInfo;
	private static Player player;

	// Use this for initialization
	void Start () {
		healthInfo = GetComponent<Text>();

		GameObject playerGameObj = GameObject.Find(Game.playerTag);
 		if (playerGameObj != null) {
    		player = playerGameObj.GetComponent<Player>();
			healthInfo.text = player.GetHealthString();
 		} else {
			Debug.Log("no player object?");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void UpdateHealthDisplay() {
		if (!Game.IsPaused()) {
			healthInfo.text = player.GetHealthString();
		} else {
			healthInfo.text = "HP: 0/" + player.GetMaxHP();
		}
	}

	public void UpdateHealthDisplayAfterUpgrading() {
		healthInfo.text = player.GetHealthString();
	}
}