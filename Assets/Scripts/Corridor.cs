using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {
    private static string exitTag = "exit";
    private string needFriendsText = "The door is too heavy. Maybe if you had some help...";
    private float messageReadTime = 2.6f;
    private float messageFadeRate = .04f;
    private float messageFadeDelay = .03f;

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
                Message.SetAndDisplayMessage(messageReadTime, messageFadeRate, messageFadeDelay, needFriendsText);
            } else {
                Game.beatGame = true;
            }
        } else {
            Debug.Log ("no player object?");
        }
    }
}