using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {

    private static string exitTag = "exit";

    void OnTriggerEnter2D (Collider2D other) {
        if ((other.gameObject.tag == Game.playerTag)) {
            if (gameObject.tag == "exit") {
                GameObject playerGameObj = GameObject.Find (Game.playerTag);
                if (playerGameObj != null) {
                    Player player = playerGameObj.GetComponent<Player> ();
                    if (player.GetNumFriends() < 1) {
                        Message needFriendsMessage = GameObject.Find(Message.needFriendsMessageName).GetComponent<Message>();
                        needFriendsMessage.ShowMessage(2.6f, .04f, .03f);
                        Debug.Log("get more friends");
                    } else {
                        Game.beatGame = true;
                        Debug.Log("you win!");
                    }
                } else {
                    Debug.Log ("no player object?");
                }
            } else {
                Game.toNextStory = true;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}