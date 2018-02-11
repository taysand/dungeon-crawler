using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFriendsText : ChangingText {
	public override void UpdateTextField () {
		Debug.Log ("updating friend display");
		textField.text = "Friends: " + player.GetNumFriends ();
	}
}