using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainSpell : Spell {

	//this is the final spell you can get

	private float percentage = .2f;
	private const float drainRate = .6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			float drained = enemy.GetHealth() * percentage;
			enemy.TakeDamage(drained);
			//TODO: get player object
			//Player.Heal(drained * drainRate);

			if (percentage > .7) {
				enemy.Sleep();
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
