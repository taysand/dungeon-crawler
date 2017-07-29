using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : Enemy
{

    private const float rockDamage = 15.0f;
    private const int rockAC = 5;
    private const float rockSpeed = 1f;
    private const float rockMaxHP = 35f;
    private const int rockLevel = 2;
    private const float rockRangeRadius = 30f;
    private const int rockSleepTime = 6;
    private const int rockFreezeTime = 4;
    private const int rockScaredTime = 1;

    protected override void SetStartingValues()
    {
        maxHP = rockMaxHP;
        hp = maxHP;
        ac = rockAC;
        level = rockLevel;
        speed = rockSpeed;
        damagePerHit = rockDamage;
        maxHP = rockMaxHP;
        rangeRadius = rockRangeRadius;
        sleepTime = rockSleepTime;
        freezeTime = rockFreezeTime;
        scaredTime = rockScaredTime;
    }
}
