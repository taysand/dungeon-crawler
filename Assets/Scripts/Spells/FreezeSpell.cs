﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSpell : Spell
{

    //can purchase after level one
    //freezes enemies 

    public Enemy enemy;//FIXME: delete after tests
    private int activeTurns = 1;//TODO: change all activeTurns into like additional sleep/freeze/scare times I guess

    // Use this for initialization
    void Start()
    {
        healthLost = 10;//TODO: make constants for starting healthLost?
    }

    // Update is called once per frame
    void Update()//FIXME: delete after tests
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Cast(enemy);
        }
    }

    protected override bool Cast(Enemy enemy)
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
