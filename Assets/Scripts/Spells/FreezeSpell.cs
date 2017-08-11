using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSpell : Spell
{
    //can purchase after level one

    private const int freezeHealthLost = 10;
    private const int freezeMaxLevelAffected = 2;

    protected override void InitializeStats()
    {
        spellName = freezeSpell;
        healthLost = freezeHealthLost;
        maxLevelAffected = freezeMaxLevelAffected;
	}

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            StartCoroutine(enemy.Freeze());
            return true;
        }
        return false;
    }
}
