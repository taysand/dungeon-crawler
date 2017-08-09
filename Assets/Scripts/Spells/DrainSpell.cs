using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSpell : Spell {

	//this is the final spell you can get

	private float percentage = .2f;
	private const float drainRate = .6f;
	private Player player;

	// Use this for initialization
	void Start () {
		 GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        }
	}

	public override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			float drained = enemy.GetHealth() * percentage;
			enemy.TakeDamage(drained);

			player.Heal(drained * drainRate);

			if (percentage > .7) {
				StartCoroutine(enemy.Sleep());
			}
			
			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();

		if (percentage < .9) {
			percentage = percentage + .1f;
		}
	}
}
