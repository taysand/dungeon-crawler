using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelevelButton : SpellButton
{

    protected override void InitializeName()
    {
        spellName = Spell.delevelSpell;
    }
}