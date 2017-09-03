using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportSpell : Spell
{
    //can purchase after level five

    private int distance = 5;
    private const int teleportHealthLost = 20;
    private const int teleportMaxLevelAffected = 4;

    protected override void InitializeStats()
    {
        spellName = teleportSpell;
        healthLost = teleportHealthLost;
        maxLevelAffected = teleportMaxLevelAffected;
    }
 
    public override bool Cast(Enemy enemy)
    {
        if (enemy.GetLevel() <= maxLevelAffected)
        {
            Debug.Log("teleporting");
            float x = enemy.transform.position.x;
            float y = enemy.transform.position.y;

            System.Random random = new System.Random();
            if (random.Next(0, 2) == 1)
            {
                x = x + distance;
            }
            else
            {
                y = y + distance;
            }
            enemy.transform.position = new Vector2(x, y);

            return true;
        }
        return false;
    }

    protected override void LevelUpSpell()
    {
        base.LevelUpSpell();
		//TODO: option to level up distance
    }
}
