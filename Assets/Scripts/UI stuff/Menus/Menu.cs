using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour {

	protected GameObject canvas;
	protected bool activated;

	public virtual void Awake() {
		canvas = GetComponent<Canvas>().gameObject;
		HideMenu();
		//TODO: this weirdly isn't actually initializing things?
	}

	public void HideMenu() {
		activated = false;
		canvas.SetActive(activated);
		Game.Unpause();
		GameplayUI.ShowGameplayUI();
	}

	public virtual void ShowMenu() {
		activated = true;
        canvas.SetActive(activated);
		Debug.Log("the canvas is " + canvas);
        GameplayUI.HideGameplayUI();
        Game.Pause();
	}
}
