using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy {

	public float skeletonDamage = 5.0f;

	public float skeletonHP = 25.0f;

	public int skeletonAC = 2;

	public float skeletonSpeed = .1f;
	public float skeletonMaxHP = 25f;

	protected override void SetStartingValues() {
		hp = skeletonHP;
		ac = skeletonAC;
		//starting level
		speed = skeletonSpeed;
		damagePerHit = skeletonDamage;
		maxHP = skeletonMaxHP;
	}
}

