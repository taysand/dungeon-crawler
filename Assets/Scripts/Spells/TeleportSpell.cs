using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : Spell {

	//can purchase after level five
	//move enemy one space away from its current location

	private int distance = 1; //starting

	// Use this for initialization
	void Start () {
		healthLost = 20; //initally, maybe
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override bool Cast(Enemy enemy) {
		//TODO: please
		if (enemy.GetLevel() <= maxLevelAffected) {
			//cast the spell
			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();

		//TODO: please
		//these should be a one or the other thing
		//if player picks distance, distance++, unless distance==10, in which case this shouldn't even be an option 
		//else, healthLost - 5, unless healthLost==10, in which case this shouldn't even be an option
	}
}
