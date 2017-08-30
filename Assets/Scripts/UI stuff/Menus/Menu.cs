using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour {

	protected GameObject canvas;
	protected bool activated;

	protected string font;
	protected Color fontColor;
	protected string upgradeParent;
	protected const string health = "health";
    protected const string armor = "armor";


	void Awake() {
		canvas = GetComponent<Canvas>().gameObject;
		
		//TODO: this weirdly isn't actually initializing things?

		AdditionalSetUp();
		BuildButtonsAndText();

		HideMenu();
	}

	protected abstract void AdditionalSetUp();

	protected abstract void BuildButtonsAndText();

	protected void BuildText(string textType) {
        GameObject o = new GameObject();
        Text text = o.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource(typeof(Font), font) as Font;
        text.color = fontColor;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;

        if (textType == health)
        {
            o.AddComponent<HealthUpgradeText>();
        } else if (textType == armor)
        {
            o.AddComponent<ArmorUpgradeText>();
        } else
        {
            Debug.Log("wrong text type");
        }
		Debug.Log("upgrade parent is: " + upgradeParent);
        o.transform.SetParent(transform.Find(upgradeParent), false);
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
