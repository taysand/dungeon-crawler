using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformSpell : Spell
{
    //turn enemy into something?

    private const int transformHealthLost = 40;
    private const int transformMaxLevelAffected = 3;

    protected override void InitializeStats()
    {
        spellName = transformSpell;
        healthLost = transformHealthLost;
        maxLevelAffected = transformMaxLevelAffected;
	}

    public override bool Cast(Enemy enemy)
    {
        //TODO: please
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            Debug.Log("transforming");
            //cast the spell
            return true;
        }
        return false;
    }
}
