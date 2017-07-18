using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScareButton : SpellButton
{

	new void Start() {
		base.Start();
	}

    protected override void InitializeName()
    {
        spellName = Spell.scareSpell;
    }
}