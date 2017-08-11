using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainSpell : Spell {

	//this is the final spell you can get
	//TODO: make this one look nice
	//TODO: add player back into spell

	private float percentage = .2f;
	private const float drainRate = .6f;
	private Player player;

	private const int drainHealthLost = 0;
    private const int drainMaxLevelAffected = 6;

    protected override void InitializeStats()
    {
        spellName = drainSpell;
        healthLost = drainHealthLost;
        maxLevelAffected = drainMaxLevelAffected;
	}

	// Use this for initialization
	void Start () {
		 GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        }
	}

	public override bool Cast(Enemy enemy) {
		if (enemy.GetLevel() <= maxLevelAffected) {
			float drained = enemy.GetHealth() * percentage;
			enemy.TakeDamage(drained);

			player.Heal(drained * drainRate);

			if (percentage > .7) {
				StartCoroutine(enemy.Sleep());
			}
			
			return true;
		} 
		return false;
	}

	protected override void LevelUpSpell() {
		base.LevelUpSpell();
		//TODO:give another option to increase drain amount
	}
}
