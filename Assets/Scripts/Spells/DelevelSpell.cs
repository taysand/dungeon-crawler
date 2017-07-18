using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelevelSpell : Spell {

	private int decreaseBy = 1; // maybe?
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			enemy.DecreaseLevel(decreaseBy);
			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();

		decreaseBy++;
	}
}
