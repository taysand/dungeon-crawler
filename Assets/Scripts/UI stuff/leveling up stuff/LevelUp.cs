using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour {

	static GameObject gm;
	static bool activated;

	private static int levelUpPoints = 1;
	private GameObject[] levelUpButtons;

	// Use this for initialization
	void Start () {
		gm = GetComponent<CanvasRenderer>().gameObject;
		levelUpButtons = GameObject.FindGameObjectsWithTag("LevelUpButton");
		HideLevelUpWindow();
	}
	
	public static bool Activated() {
		return activated;
	}

	public static void ShowLevelUpWindow() {
		Game.Pause();
		activated = true;
		gm.SetActive(true);
		//SpellsList.UpdateSpellsList();
	}

	public static void StaticHideLevelUpWindow() {
		gm.SetActive(false);
		activated = false;
		Game.Unpause();
	}

	public void HideLevelUpWindow() {
		gm.SetActive(false);
		activated = false;
		Game.Unpause();
	}

	public void HideEverything() {
		LUText.HideAnnouncement();
		LUImage.HideAnnouncement();
		HideLevelUpWindow();
	}

	public static void GainLevelUpPoint() {
		levelUpPoints++;
	}

	public static int GetLevelUpPoints() {
		return levelUpPoints;
	}

	public void SpendLevelUpPoints() {
		levelUpPoints--;
	}

	void Update() {
		if (levelUpPoints > 0) {
			foreach (GameObject b in levelUpButtons) {
				Button button = b.GetComponent<Button>();
				button.interactable = true;
			}
		}

		DisplayLevel.UpdateDisplayedLevel();

		if (levelUpPoints == 0) {
			foreach (GameObject b in levelUpButtons) {
				Button button = b.GetComponent<Button>();
				button.interactable = false;
			}
		}
	}
}
