using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy {

	public float mushroomStartingDamage = 5.0f;//should be constant
	public float mushroomStartingHP = 20.0f;//should be constant
	public int mushroomStartingAC = 1;//should be constant
	public float mushroomStartingSpeed = 2f;//should be constant
	public float mushroomStartingMaxHP = 20f;//should be constant
	public int mushroomStartingLevel = 3;//should be constant

	protected override void SetStartingValues() {
		hp = mushroomStartingHP;
		ac = mushroomStartingAC;
		level = mushroomStartingLevel;
		speed = mushroomStartingSpeed;
		damagePerHit = mushroomStartingDamage;
		maxHP = mushroomStartingMaxHP;
	}
}
