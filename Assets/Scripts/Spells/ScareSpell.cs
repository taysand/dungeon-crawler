using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareSpell : Spell {

	//can purchase after level three

	private int activeTurns = 1;

	// Use this for initialization
	void Start () {
		healthLost = 15;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			enemy.Scare();
			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();

		//if player picks activeTurns, activeTurns++, unless activeTurns==7, in which case this shouldn't even be an option 
		//else, healthLost - 5, unless healthLost==7, in which case this shouldn't even be an option
	}
}
