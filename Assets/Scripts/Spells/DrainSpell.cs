using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainSpell : Spell
{
    //this is the final spell you can get

    private float percentage = .2f;
    private const float drainRate = .6f;

    private const int drainHealthLost = 0;
    private const int drainMaxLevelAffected = 6;

    protected override void InitializeStats()
    {
        spellName = drainSpell;
        healthLost = drainHealthLost;
        maxLevelAffected = drainMaxLevelAffected;
    }

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            Debug.Log("draining health");
            float drained = enemy.GetHealth() * percentage;
            enemy.TakeDamage(drained);

            player.Heal(drained * drainRate);

            if (percentage > .7)
            {
                StartCoroutine(enemy.Sleep(0f));
            }

            return true;
        }
        return false;
    }

    protected override void LevelUpSpell()
    {
        base.LevelUpSpell();
        //TODO:give another option to increase drain precentage
    }
}
