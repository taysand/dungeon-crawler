using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	public float skeletonStartingDamage = 5.0f;//should be constant
	public float skeletonStartingHP = 25.0f;//should be constant
	public int skeletonStartingAC = 2;//should be constant
	public float skeletonStartingSpeed = 10f;//should be constant
	public float skeletonStartingMaxHP = 25f;//should be constant
	public int skeletonStartingLevel = 1;//should be constant

	protected override void SetStartingValues() {
		hp = skeletonStartingHP;
		ac = skeletonStartingAC;
		level = skeletonStartingLevel;
		speed = skeletonStartingSpeed;
		damagePerHit = skeletonStartingDamage;
		maxHP = skeletonStartingMaxHP;
	}
}

