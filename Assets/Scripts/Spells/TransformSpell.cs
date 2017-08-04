using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSpell : Spell {

//turn enemy into something?

	// Use this for initialization
	void Start () {
		healthLost = 30; //initally, maybe
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool Cast(Enemy enemy) {
		//TODO: please
		if (enemy.GetLevel() <= maxLevelAffected) {
			//cast the spell
			return true;
		} 
		return false;
	}
}
