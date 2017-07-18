using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformButton : SpellButton
{

    protected override void InitializeName()
    {
        spellName = Spell.transformSpell;
    }
}