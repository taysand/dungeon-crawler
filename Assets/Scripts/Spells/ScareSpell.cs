using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareSpell : Spell {
    private const int scareHealthLost = 5;
    private const int scareMaxLevelAffected = 2;
    private int additionalScareTime = 0;
    private int additionalScareDistance = 0;
    private int scareRequiredLevel = 2;

    protected override void InitializeStats () {
        spellName = scareSpell;
        healthLost = scareHealthLost;
        maxLevelAffected = scareMaxLevelAffected;
        requiredLevel = scareRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            enemy.Scare (additionalScareTime, additionalScareDistance);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}