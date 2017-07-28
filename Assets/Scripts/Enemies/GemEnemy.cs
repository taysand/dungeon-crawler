using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemEnemy : Enemy
{

    private const float gemStartingDamage = 10.0f;//should be constant
    private const float gemStartingHP = 30.0f;//should be constant
    private const int gemStartingAC = 3;//should be constant
    private const float gemStartingSpeed = 1f;//should be constant
    private const float gemStartingMaxHP = 30f;//should be constant
    private const int gemStartingLevel = 4;//should be constant
    private const float gemStartingRangeRadius = 20f;//should be constant 

    protected override void SetStartingValues()
    {
        hp = gemStartingHP;
        ac = gemStartingAC;
        level = gemStartingLevel;
        speed = gemStartingSpeed;
        damagePerHit = gemStartingDamage;
        maxHP = gemStartingMaxHP;
        rangeRadius = gemStartingRangeRadius;
    }
}
