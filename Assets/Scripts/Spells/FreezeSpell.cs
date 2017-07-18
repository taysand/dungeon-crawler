using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : Spell {

	//can purchase after level one
	//freezes an enemy for one turn	

	private int activeTurns = 1;

	// Use this for initialization
	void Start () {
		healthLost = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			enemy.Freeze();
			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();

		if (activeTurns < 5) {
			activeTurns++;
		}
	}
}
