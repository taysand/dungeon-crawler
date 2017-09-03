using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{

    private const float skeletonDamage = 5.0f;
    private const float skeletonHP = 25.0f;
    private const int skeletonAC = 2;
    private const float skeletonSpeed = 3f;
    private const float skeletonMaxHP = 25f;
    private const int skeletonLevel = 1;
    private const float skeletonRangeRadius = 40f;
    private const int skeletonSleepTime = 3;
    private const int skeletonFreezeTime = 5;
    private const int skeletonScaredTime = 4;
    private const int skeletonScaredDistance = 10;

    protected override void SetStartingValues()
    {
        currentMaxHP = skeletonMaxHP;
        hp = currentMaxHP;
        ac = skeletonAC;
        level = skeletonLevel;
        speed = skeletonSpeed;
        damagePerHit = skeletonDamage;
        rangeRadius = skeletonRangeRadius;
		sleepTime = skeletonSleepTime;
		freezeTime = skeletonFreezeTime;
		scaredTime = skeletonScaredTime;
        scaredDistance = skeletonScaredDistance;
    }
}

