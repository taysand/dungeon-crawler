using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepButton : SpellButton
{

    protected override void InitializeName()
    {
        spellName = Spell.sleepSpell;
    }
}