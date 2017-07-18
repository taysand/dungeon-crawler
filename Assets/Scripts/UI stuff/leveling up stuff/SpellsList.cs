using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsList : MonoBehaviour {

	static Text spellsField;
	
	// Use this for initialization
	void Start () {
		spellsField = GetComponent<Text>();

		Player.ResetSpellList();
		
	

		//System.Threading.Thread.Sleep(5000);

		

		//UpdateSpellsList();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void UpdateSpellsList() {
		//take out this spell additions obviously 
		Player.LearnSpell("delevel");
		Player.LearnSpell("freeze");
		Player.LearnSpell("teleport");

		//known spells
		List<string> spellsList = Player.GetKnownSpells();
		string spells = "";
		for (int i = 0; i < spellsList.Count; i++) {
			spells = spells + spellsList[i] + "\n";
		}
		spellsField.text = "Known spells:\n" + spells;

		//unknown spells
		List<string> spellsList2 = Player.GetUnknownSpells();
		string spells2 = "";
		for (int i = 0; i < spellsList2.Count; i++) {
			spells2 = spells2 + spellsList2[i] + "\n";
		}
		Debug.Log("This should be a list of unknown spells: " + spells2);
		spellsField.text = spellsField.text + "\n\nUnknown spells:\n" + spells2;
	}
}
