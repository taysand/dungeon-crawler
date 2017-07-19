using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy {

	public float mushroomDamage = 5.0f;

	public float mushroomHP = 20.0f;

	public int mushroomAC = 1;

	public float mushroomSpeed = .1f;
	public float mushroomMaxHP = 20f;

	protected override void SetStartingValues() {
		hp = mushroomHP;
		ac = mushroomAC;
		//starting level
		speed = mushroomSpeed;
		damagePerHit = mushroomDamage;
		maxHP = mushroomMaxHP;
	}
}
