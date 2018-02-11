using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformSpell : Spell {
    private const int transformHealthLost = 40;
    private const int transformMaxLevelAffected = 3;
    private int transformRequiredLevel = 6;

    protected override void InitializeStats () {
        spellName = transformSpell;
        healthLost = transformHealthLost;
        maxLevelAffected = transformMaxLevelAffected;
        requiredLevel = transformRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            //cast the spell
            return true;
        }
        return false;
    }
}