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

	//for level up
	public GameObject plusButtonPrefab;//not great because it's everywhere
	protected const string health = "health";
    protected const string armor = "armor";
	public LevelUpMenu lUM;//also not great because it's everywhere
	protected HealthUpgradeText healthUpgradeText;
	protected ArmorUpgradeText armorUpgradeText;

	void Awake() {
		canvas = GetComponent<Canvas>().gameObject;
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
           healthUpgradeText = o.AddComponent<HealthUpgradeText>();
        } else if (textType == armor)
        {
            armorUpgradeText = o.AddComponent<ArmorUpgradeText>();
        } else
        {
            Debug.Log("wrong text type");
        }
		Debug.Log("upgrade parent is: " + upgradeParent);
        o.transform.SetParent(transform.Find(upgradeParent), false);
    }

	protected void BuildButton(string buttonType) {
        GameObject button = Instantiate(plusButtonPrefab) as GameObject;
        if (buttonType == health)
        {
            button.GetComponent<Button>().onClick.AddListener(lUM.IncreaseHealth);
        } else if (buttonType == armor)
        {
            button.GetComponent<Button>().onClick.AddListener(lUM.IncreaseArmor);
        } else
        {
            Debug.Log("wrong button type");
        }
        button.transform.SetParent(transform.Find(upgradeParent), false);
        Transform childText = button.transform.Find("Text");
        Destroy(childText.gameObject);
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

	public bool Activated() {
		return activated;
	}
}
