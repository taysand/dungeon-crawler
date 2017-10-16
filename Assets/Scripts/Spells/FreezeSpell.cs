using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezeSpell : Spell
{
    //can purchase after level one

    private const int freezeHealthLost = 10;
    private const int freezeMaxLevelAffected = 10;
    private int additionalFreezeTime = 0;

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
            // Debug.Log("freezing");
            enemy.Freeze(additionalFreezeTime);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell()
    {
        base.LevelUpSpell();
        //TODO:give another option to increase freeze time
    }
}
