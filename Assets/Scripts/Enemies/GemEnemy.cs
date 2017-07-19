using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEnemy : Enemy {

	public float gemDamage = 10.0f;
	public float gemHP = 30.0f;
	public int gemAC = 3;
	public float gemSpeed = .07f;
	public float gemMaxHP = 30f;

	protected override void SetStartingValues() {
		hp = gemHP;
		ac = gemAC;
		//starting level
		speed = gemSpeed;
		damagePerHit = gemDamage;
		maxHP = gemMaxHP;
	}
}
