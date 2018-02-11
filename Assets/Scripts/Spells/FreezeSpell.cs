using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSpell : Spell {
    private const int freezeHealthLost = 10;
    private const int freezeMaxLevelAffected = 3;
    private int additionalFreezeTime = 0;
    private int freezeRequiredLevel = 1;

    protected override void InitializeStats () {
        spellName = freezeSpell;
        healthLost = freezeHealthLost;
        maxLevelAffected = freezeMaxLevelAffected;
        requiredLevel = freezeRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            enemy.Freeze (additionalFreezeTime);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}