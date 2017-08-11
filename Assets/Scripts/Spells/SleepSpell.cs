using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepSpell : Spell
{
    //can purchase after level one

    private const int sleepHealthLost = 10;
    private const int sleepMaxLevelAffected = 3;

    protected override void InitializeStats() {
        spellName = sleepSpell;
        healthLost = sleepHealthLost;
        maxLevelAffected = sleepMaxLevelAffected;
    }

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            StartCoroutine(enemy.Sleep());
            return true;
        }
        return false;
    }
}
