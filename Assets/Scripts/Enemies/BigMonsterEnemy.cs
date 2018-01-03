using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterEnemy : Enemy
{
    private const float bigMonsterDamage = 30.0f;
    private const int bigMonsterAC = 7;
    private const float bigMonsterSpeed = 2f;
    private const float bigMonsterMaxHP = 50f;
    private const int bigMonsterLevel = 10;
    private const float bigMonsterRangeRadius = 20f;
    private const int bigMonsterSleepTime = 1;
    private const int bigMonsterFreezeTime = 1;
    private const int bigMonsterScaredTime = 1;

    protected override void SetStartingValues()
    {
        currentMaxHP = bigMonsterMaxHP;
        hp = currentMaxHP;
        ac = bigMonsterAC;
        level = bigMonsterLevel;
        speed = bigMonsterSpeed;
        damagePerHit = bigMonsterDamage;     
        rangeRadius = bigMonsterRangeRadius;
        sleepTime = bigMonsterSleepTime;
        freezeTime = bigMonsterFreezeTime;
        scaredTime = bigMonsterScaredTime;
    }
}
