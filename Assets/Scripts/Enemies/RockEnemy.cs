using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : Enemy {

	public float rockStartingDamage = 15.0f;//should be constant
	public float rockStartingHP = 35.0f;//should be constant
	public int rockStartingAC = 5;//should be constant
	public float rockStartingSpeed = .08f;//should be constant
	public float rockStartingMaxHP = 35f;//should be constant
	public int rockStartingLevel = 2;//should be constant

	protected override void SetStartingValues() {
		hp = rockStartingHP;
		ac = rockStartingAC;
		level = rockStartingLevel;
		speed = rockStartingSpeed;
		damagePerHit = rockStartingDamage;
		maxHP = rockStartingMaxHP;
	}
}
