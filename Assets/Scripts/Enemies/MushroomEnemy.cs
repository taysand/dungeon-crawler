using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy
{
    private const float mushroomDamage = 5.0f;
    private const int mushroomAC = 1;
    private const float mushroomSpeed = 3f;
    private const float mushroomMaxHP = 20f;
    private const int mushroomLevel = 3;
    private const float mushroomRangeRadius = 50f;
    private const int mushroomSleepTime = 2;
    private const int mushroomFreezeTime = 4;
    private const int mushroomScaredTime = 3;

    protected override void SetStartingValues()
    {
        currentMaxHP = mushroomMaxHP;
        hp = currentMaxHP;
        ac = mushroomAC;
        level = mushroomLevel;
        speed = mushroomSpeed;
        damagePerHit = mushroomDamage;
        rangeRadius = mushroomRangeRadius;
        sleepTime = mushroomSleepTime;
        freezeTime = mushroomFreezeTime;
        scaredTime = mushroomScaredTime;
    }
}
