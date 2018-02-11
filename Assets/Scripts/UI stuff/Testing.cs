using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour {
	Text text;

	void Start () {
		text = GetComponent<Text> ();
	}

	void Update () {
		if (Game.IsPaused ()) {
			text.text = "paused";
		} else {
			text.text = "unpaused";
		}
	}
}