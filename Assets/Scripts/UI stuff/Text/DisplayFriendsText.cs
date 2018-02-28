using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayFriendsText : ChangingText {
	public override void UpdateTextField () {
		textField.text = "Friends: " + player.GetNumFriends ();
	}
}