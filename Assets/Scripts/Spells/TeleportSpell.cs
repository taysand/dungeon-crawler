using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpell : Spell {

	//can purchase after level five
	//move enemy one space away from its current location

	private int distance = 5; //starting

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
			float x = enemy.transform.position.x;
			float y = enemy.transform.position.y;

			System.Random random = new System.Random();
			if (random.Next(0, 2) == 1) {
				x = x + distance;
			} else {
				y = y + distance;
			}
			enemy.transform.position = new Vector2(x, y);

			return true;
		} 
		return false;
	}

	protected override void LevelUp() {
		base.LevelUp();
	}
}
