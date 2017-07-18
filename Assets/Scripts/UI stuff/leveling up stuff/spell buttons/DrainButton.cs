using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainButton : SpellButton
{

    protected override void InitializeName()
    {
        spellName = Spell.drainSpell;
    }
}