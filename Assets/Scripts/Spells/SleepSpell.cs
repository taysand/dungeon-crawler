using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepSpell : Spell {
    private const int sleepHealthLost = 3;
    private const int sleepMaxLevelAffected = 3;
    private const int additionalSleepTime = 0;
    private int sleepRequiredLevel = 1;

    protected override void InitializeStats () {
        spellName = sleepSpell;
        healthLost = sleepHealthLost;
        maxLevelAffected = sleepMaxLevelAffected;
        requiredLevel = sleepRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            enemy.Sleep (additionalSleepTime);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}