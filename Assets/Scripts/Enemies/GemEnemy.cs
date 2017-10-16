using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEnemy : Enemy
{

    private const float gemDamage = 10.0f;
    private const int gemAC = 3;
    private const float gemSpeed = 1f;
    private const float gemMaxHP = 30f;
    private const int gemLevel = 4;
    private const float gemRangeRadius = 20f;
    private const int gemSleepTime = 4;
    private const int gemFreezeTime = 1;
    private const int gemScaredTime = 6;
    // private const int gemScaredDistance = 15;

    protected override void SetStartingValues()
    {
        currentMaxHP = gemMaxHP;
        hp = currentMaxHP;
        ac = gemAC;
        level = gemLevel;
        speed = gemSpeed;
        damagePerHit = gemDamage;     
        rangeRadius = gemRangeRadius;
        sleepTime = gemSleepTime;
        freezeTime = gemFreezeTime;
        scaredTime = gemScaredTime;
        // scaredDistance = gemScaredDistance;
    }
}
