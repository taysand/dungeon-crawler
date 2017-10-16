using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelevelSpell : Spell
{
    private int decreaseBy = 1;
    private const int delevelHealthLost = 10;
    private const int delevelMaxLevelAffected = 20;

    protected override void InitializeStats()
    {
        spellName = delevelSpell;
        healthLost = delevelHealthLost;
        maxLevelAffected = delevelMaxLevelAffected;
    }

    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            // Debug.Log("reducing level");
            enemy.DecreaseLevel(decreaseBy);
            return true;
        }
        return false;
    }

    protected override void LevelUpSpell()
    {
        base.LevelUpSpell();
		//TODO: option to increase the levels lost
    }
}
