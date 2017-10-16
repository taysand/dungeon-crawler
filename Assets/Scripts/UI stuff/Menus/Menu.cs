using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{

    protected GameObject canvas;
    protected bool activated;

    protected string font = "Arial.ttf";
    protected Color fontColor;
    protected const int titleFontSize = 25;

    //for level up
    protected const string health = "health";
    protected const string armor = "armor";
    protected const string levelInfo = "level info";
    protected const string done = "Done";
    public LevelUpMenu lUM;//also not great because it's everywhere. TODO: maybe make it just Menu and then have it be the menu that I'm dealing with? or just have a Menu be an argument to the button thing
    public PauseMenu pM; //TODO: also bad
    protected static HealthUpgradeText healthUpgradeText;
    protected static ArmorUpgradeText armorUpgradeText;
    protected static DisplayLevelText levelDisplay;

    //for game over
    protected const string restart = "Restart";
    protected const string mainMenu = "Main Menu";

    //for pause
    protected const string pauseTitle = "pause title";
    protected const string resume = "Resume";
    protected const string levelUp = "Open Level Up";
    protected const string settings = "Settings";
    private const string pauseTitleText = "Paused";

    void Awake() {
        canvas = GetComponent<Canvas>().gameObject;
        fontColor = new Color(.57f, .08f, 1f, 1f);
        AdditionalSetUp();
        BuildButtonsAndText();
        HideMenu();
    }

    protected abstract void AdditionalSetUp();

    protected abstract void BuildButtonsAndText();

    protected void BuildText(string textType, Transform parent) {
        GameObject o = new GameObject();
        Text text = o.AddComponent<Text>();
        text.font = Resources.GetBuiltinResource(typeof(Font), font) as Font;
        text.color = fontColor;
        text.fontStyle = FontStyle.Bold;
        text.alignment = TextAnchor.MiddleCenter;

        switch (textType)
        {
            case health:
                healthUpgradeText = o.AddComponent<HealthUpgradeText>();
                break;
            case armor:
                armorUpgradeText = o.AddComponent<ArmorUpgradeText>();
                break;
            case levelInfo:
                text.fontSize = titleFontSize;
                text.resizeTextForBestFit = true;
                levelDisplay = o.AddComponent<DisplayLevelText>();
                break;
            case pauseTitle:
                text.fontSize = titleFontSize;
                text.text = pauseTitleText;
                break;
            default:
                Debug.Log("wrong text type");
                break;
        }

        o.transform.SetParent(parent, false);
    }

    protected void BuildButton(string buttonType, Transform parent, GameObject buttonPrefab) {
        GameObject button = Instantiate(buttonPrefab) as GameObject;

        Button actualButton = button.GetComponent<Button>();

        switch (buttonType)
        {
            case health:
                actualButton.onClick.AddListener(lUM.IncreaseHealth);
                break;
            case armor:
                actualButton.onClick.AddListener(lUM.IncreaseArmor);
                break;
            case done:
                actualButton.onClick.AddListener(lUM.HideMenu);
                break;
            case mainMenu:
                actualButton.onClick.AddListener(pM.GoToMainMenu);
                break;
            case restart:
                actualButton.onClick.AddListener(pM.RestartLevel);
                break;
            case resume:
                actualButton.onClick.AddListener(pM.HideMenu);
                break;
            case levelUp:
                actualButton.onClick.AddListener(pM.HideMenu);
                actualButton.onClick.AddListener(lUM.ShowMenu);
                break;
            case settings:
                actualButton.onClick.AddListener(pM.OpenSettings);
                break;
            default:
                Debug.Log("wrong button type: " + buttonType);
                break;
        }

        button.transform.SetParent(parent, false);

        Transform child = button.transform.GetChild(0);
        if (child != null)
        {
            Text text = child.GetComponent<Text>();
            if (text != null)
            {
                text.text = buttonType;
            } else
            {
                Debug.Log("button text is null");
            }
        } else
        {
            Debug.Log("button has no children");
        }

        Transform childText = button.transform.Find("Text");
        if (childText != null)
        {
            Destroy(childText.gameObject);
        }
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
        // Debug.Log("the canvas is " + canvas);
        GameplayUI.HideGameplayUI();
        Game.Pause();
    }

    public bool Activated() {
        return activated;
    }
}
