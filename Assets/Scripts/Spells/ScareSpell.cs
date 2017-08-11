using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareSpell : Spell
{
    //can purchase after level three

    private const int scareHealthLost = 15;
    private const int scareMaxLevelAffected = 2;

    protected override void InitializeStats()
    {
        spellName = scareSpell;
        healthLost = scareHealthLost;
        maxLevelAffected = scareMaxLevelAffected;
    }

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            StartCoroutine(enemy.Scare());
            return true;
        }
        return false;
    }
}
