using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : Enemy {

	public float rockDamage = 15.0f;
	public float rockHP = 35.0f;
	public int rockAC = 5;
	public float rockSpeed = .08f;
	public float rockMaxHP = 35f;

	protected override void SetStartingValues() {
		hp = rockHP;
		ac = rockAC;
		//starting level
		//animator
		speed = rockSpeed;
		damagePerHit = rockDamage;
		maxHP = rockMaxHP;
	}
}
