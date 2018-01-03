using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepSpell : Spell
{
    //can purchase after level one

    private const int sleepHealthLost = 10;
    private const int sleepMaxLevelAffected = 3;
    private const int additionalSleepTime = 0;
    private int sleepRequiredLevel = 1;

    protected override void InitializeStats() {
        spellName = sleepSpell;
        healthLost = sleepHealthLost;
        maxLevelAffected = sleepMaxLevelAffected;
        requiredLevel = sleepRequiredLevel;
    }

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            // Debug.Log("putting enemy to sleep");
            enemy.Sleep(additionalSleepTime);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell()
    {
        base.LevelUpSpell();
        //TODO:give another option to increase sleep time
    }
}
