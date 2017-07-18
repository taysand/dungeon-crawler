using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

	protected int healthLost;
	protected int maxLevelAffected = 5; //is this good? who knows
	protected int level = 0;

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
		allSpells.Add("delevel");
		allSpells.Add("drain");
		allSpells.Add("freeze");
		allSpells.Add("scare");
		allSpells.Add("teleport");
		allSpells.Add("transform");
		return allSpells;
	}
}
