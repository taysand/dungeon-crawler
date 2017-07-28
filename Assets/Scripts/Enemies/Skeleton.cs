using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	private const float skeletonStartingDamage = 5.0f;//should be constant
	private const float skeletonStartingHP = 25.0f;//should be constant
	private const int skeletonStartingAC = 2;//should be constant
	private const float skeletonStartingSpeed = 3f;//should be constant
	private const float skeletonStartingMaxHP = 25f;//should be constant
	private const int skeletonStartingLevel = 1;//should be constant
	private const float skeletonStartingRangeRadius = 40f;//should be constant 

	protected override void SetStartingValues() {
		hp = skeletonStartingHP;
		ac = skeletonStartingAC;
		level = skeletonStartingLevel;
		speed = skeletonStartingSpeed;
		damagePerHit = skeletonStartingDamage;
		maxHP = skeletonStartingMaxHP;
		rangeRadius = skeletonStartingRangeRadius;
	}
}

