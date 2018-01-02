using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == Game.playerTag))
        {
			Game.toNextStory = true;
        }
    }
}
