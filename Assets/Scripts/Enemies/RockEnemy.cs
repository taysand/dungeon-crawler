using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : Enemy
{

    private const float rockStartingDamage = 15.0f;//should be constant
    private const float rockStartingHP = 35.0f;//should be constant
    private const int rockStartingAC = 5;//should be constant
    private const float rockStartingSpeed = 1f;//should be constant
    private const float rockStartingMaxHP = 35f;//should be constant
    private const int rockStartingLevel = 2;//should be constant
    private const float rockStartingRangeRadius = 30f;//should be constant 

    protected override void SetStartingValues()
    {
        hp = rockStartingHP;
        ac = rockStartingAC;
        level = rockStartingLevel;
        speed = rockStartingSpeed;
        damagePerHit = rockStartingDamage;
        maxHP = rockStartingMaxHP;
        rangeRadius = rockStartingRangeRadius;
    }
}
