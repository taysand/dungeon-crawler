using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {
    private static string exitTag = "exit";

    void OnTriggerEnter2D (Collider2D other) {
        if ((other.gameObject.tag == Game.playerTag)) {
            Game.toNextStory = true;
            gameObject.GetComponent<Collider2D> ().enabled = false;
        }
    }

    void OnCollisionEnter2D (Collision2D other) {
        GameObject playerGameObj = GameObject.Find (Game.playerTag);
        if (playerGameObj != null) {
            Player player = playerGameObj.GetComponent<Player> ();
            if (player.GetNumFriends () < Game.requiredFriendsToWin) {
                Message needFriendsMessage = GameObject.Find (Message.needFriendsMessageName).GetComponent<Message> ();
                needFriendsMessage.ShowMessage (2.6f, .04f, .03f);
            } else {
                Game.beatGame = true;
            }
        } else {
            Debug.Log ("no player object?");
        }
    }
}