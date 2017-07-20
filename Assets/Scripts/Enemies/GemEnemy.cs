using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEnemy : Enemy {

	public float gemStartingDamage = 10.0f;//should be constant
	public float gemStartingHP = 30.0f;//should be constant
	public int gemStartingAC = 3;//should be constant
	public float gemStartingSpeed = .07f;//should be constant
	public float gemStartingMaxHP = 30f;//should be constant
	public int gemStartingLevel = 4;//should be constant

	protected override void SetStartingValues() {
		hp = gemStartingHP;
		ac = gemStartingAC;
		level = gemStartingLevel;
		speed = gemStartingSpeed;
		damagePerHit = gemStartingDamage;
		maxHP = gemStartingMaxHP;
	}
}
