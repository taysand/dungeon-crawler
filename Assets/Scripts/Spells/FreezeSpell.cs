using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : Spell
{

    //can purchase after level one
    //freezes enemies 

    private int activeTurns = 1;//TODO: change all activeTurns into like additional sleep/freeze/scare times I guess

    // Use this for initialization
    void Start()
    {
        healthLost = 10;//TODO: make constants for starting healthLost?
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

    protected override void LevelUp()
    {
        base.LevelUp();

        if (activeTurns < 5)
        {
            activeTurns++;
        }
    }
}
