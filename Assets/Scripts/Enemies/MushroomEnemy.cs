using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy
{

    private const float mushroomStartingDamage = 5.0f;//should be constant
    private const float mushroomStartingHP = 20.0f;//should be constant
    private const int mushroomStartingAC = 1;//should be constant
    private const float mushroomStartingSpeed = 2f;//should be constant
    private const float mushroomStartingMaxHP = 20f;//should be constant
    private const int mushroomStartingLevel = 3;//should be constant
    private const float mushroomStartingRangeRadius = 10f;//should be constant 

    protected override void SetStartingValues()
    {
        hp = mushroomStartingHP;
        ac = mushroomStartingAC;
        level = mushroomStartingLevel;
        speed = mushroomStartingSpeed;
        damagePerHit = mushroomStartingDamage;
        maxHP = mushroomStartingMaxHP;
        rangeRadius = mushroomStartingRangeRadius;
    }
}
