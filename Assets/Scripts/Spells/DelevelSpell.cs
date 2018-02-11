using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelevelSpell : Spell {
    private int decreaseBy = 1;
    private const int delevelHealthLost = 10;
    private const int delevelMaxLevelAffected = 20;
    private int delevelRequiredLevel = 4;

    protected override void InitializeStats () {
        spellName = delevelSpell;
        healthLost = delevelHealthLost;
        maxLevelAffected = delevelMaxLevelAffected;
        requiredLevel = delevelRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            enemy.DecreaseLevel (decreaseBy);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}