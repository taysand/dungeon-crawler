using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

	protected int healthLost;
	protected int maxLevelAffected = 5; //is this good? who knows
	protected int level = 0;

	//spell name constants
	public const string delevelSpell = "Reduce Level";
	public const string drainSpell = "HP Drain";
	public const string freezeSpell = "Freeze";
	public const string scareSpell = "Scare";
	public const string teleportSpell = "Teleport";
	public const string transformSpell = "Transform";
	public const string sleepSpell = "Sleep";

	public static List<string> allSpells = new List<string>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected abstract bool Cast(Enemy enemy);

	public int GetHealthLost() {
		return healthLost;
	}

	protected virtual void LevelUp() {
		level++;
		maxLevelAffected++;
	}

	public static List<string> CreateSpellList() {
		allSpells.Add(delevelSpell);
		allSpells.Add(drainSpell);
		allSpells.Add(freezeSpell);
		allSpells.Add(scareSpell);
		allSpells.Add(teleportSpell);
		allSpells.Add(transformSpell);
		allSpells.Add(sleepSpell);
		return allSpells;
	}
}
