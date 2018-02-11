using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainSpell : Spell {
    private float percentage = .2f;
    private const float drainRate = .6f;

    private const int drainHealthLost = 0;
    private const int drainMaxLevelAffected = 6;
    private int drainRequiredLevel = 7;

    protected override void InitializeStats () {
        spellName = drainSpell;
        healthLost = drainHealthLost;
        maxLevelAffected = drainMaxLevelAffected;
        requiredLevel = drainRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            float drained = enemy.GetHealth () * percentage;
            enemy.TakeDamage (drained);

            player.Heal (drained * drainRate);

            if (percentage >.7) {
                enemy.Sleep (0);
            }

            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}