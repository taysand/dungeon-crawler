using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSpell : Spell
{

    //can purchase after level one
    //puts enemies to sleep

    private int activeTurns = 1;

    // Use this for initialization
    void Start()
    {
        healthLost = 10;
    }

    protected override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            StartCoroutine(enemy.Sleep());
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
