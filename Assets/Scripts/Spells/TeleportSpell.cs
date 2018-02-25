using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSpell : Spell {
    private int distance = 5;
    private const int teleportHealthLost = 7;
    private const int teleportMaxLevelAffected = 4;
    private int teleportRequiredLevel = 5;

    protected override void InitializeStats () {
        spellName = teleportSpell;
        healthLost = teleportHealthLost;
        maxLevelAffected = teleportMaxLevelAffected;
        requiredLevel = teleportRequiredLevel;
    }

    public override bool Cast (Enemy enemy) {
        if (enemy.GetLevel () <= maxLevelAffected) {
            float x = enemy.transform.position.x;
            float y = enemy.transform.position.y;

            System.Random random = new System.Random ();
            if (random.Next (0, 2) == 1) {
                x = x + distance;
                y = y + distance;
            } else {
                x = x - distance;
                y = y - distance;
            }
            enemy.Teleport ((int) x, (int) y);

            return true;
        }
        return false;
    }

    protected override void LevelUpSpell () {
        base.LevelUpSpell ();
    }
}