using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButtons : MonoBehaviour
{
    //adding buttons: http://answers.unity3d.com/questions/875588/unity-ui-dynamic-buttons.html and https://unity3d.com/learn/tutorials/topics/user-interface-ui/adding-buttons-script
    private List<string> allSpellNames = new List<string>();
    private static List<Button> allSpellButtons = new List<Button>();
	private static List<Button> allCastingButtons = new List<Button>();

    public GameObject castingButtonPrefab;
    public GameObject levelUpButtonPrefab;
    public RectTransform castingParent;
    public RectTransform levelUpParent;

    private Text numberText;
    private Text nameText;

    private const int nameIndex = 1;
    private const int numberIndex = 0;

    //button tag names
    public const string levelUpButtonTag = "LevelUpButton";
    public const string castSpellButtonTag = "CastSpellButton";

    void Start()
    {
        allSpellNames = Spell.GetAllSpellNames();
        AddButtons(castingButtonPrefab);
        AddButtons(levelUpButtonPrefab);
        FinishButtons();
    }

    private void AddButtons(GameObject prefab)
    {
        for (int i = 0; i < allSpellNames.Count; i++)
        {
            string spellName = allSpellNames[i];
            GameObject button = Instantiate(prefab) as GameObject;

            switch (spellName)
            {
                case Spell.delevelSpell:
                    button.AddComponent<DelevelSpell>();
                    break;
                case Spell.drainSpell:
                    button.AddComponent<DrainSpell>();
                    break;
                case Spell.freezeSpell:
                    button.AddComponent<FreezeSpell>();
                    break;
                case Spell.scareSpell:
                    button.AddComponent<ScareSpell>();
                    break;
                case Spell.teleportSpell:
                    button.AddComponent<TeleportSpell>();
                    break;
                case Spell.transformSpell:
                    button.AddComponent<TransformSpell>();
                    break;
                case Spell.sleepSpell:
                    button.AddComponent<SleepSpell>();
                    break;
                default:
                    Debug.Log("spell name not found");
                    break;
            }

            Button buttonInstance = button.GetComponent<Button>();

            allSpellButtons.Add(buttonInstance);
        }
    }

    private void FinishButtons()
    {
        for (int i = 0; i < allSpellButtons.Count; i++)
        {
            Button button = allSpellButtons[i];
            
            if (button.tag == castSpellButtonTag)
            {
                button.transform.SetParent(castingParent, false);

                GameObject numberSpot = button.transform.GetChild(numberIndex).gameObject;
                if (numberSpot != null)
                {
                    numberSpot.GetComponent<Text>().text = "" + i;
                }
                else
                {
                    Debug.Log("no number text spot");
                }

                UpdateSpellSlots(button);

                //https://forum.unity3d.com/threads/why-wont-onclick-addlistener-accept-a-field.357791/
                button.onClick.AddListener(button.GetComponent<Spell>().OnCastingClick);

				allCastingButtons.Add(button);
            }
            else if (button.tag == levelUpButtonTag)
            {
                button.transform.SetParent(levelUpParent, false);
                CheckIfKnown(button);
                button.onClick.AddListener(() => button.GetComponent<Spell>().OnLevelUpClick(button));
            }
        }
    }

    public static void UpdateSpellSlots(Button button)
    {
        GameObject nameSpot = button.transform.GetChild(nameIndex).gameObject;
        string spellName = button.GetComponent<Spell>().GetSpellName();
        if (nameSpot != null)
        {
            Text nameText = nameSpot.GetComponent<Text>();
            if (Player.SpellIsKnown(spellName))
            {
                nameText.text = spellName;
                button.interactable = true;
            }
            else
            {
                nameText.text = "Unknown";
                button.interactable = false;
            }
        }
        else
        {
            Debug.Log("no name text spot");
        }
    }

    public static void CheckIfKnown(Button button)
    {
        Text name = button.GetComponentInChildren<Text>();
        string spellName = button.GetComponent<Spell>().GetSpellName();
        if (Player.SpellIsKnown(spellName))
        {
            name.text = spellName + ": Learned!";
            button.interactable = false;
        }
        else
        {
            name.text = spellName;
        }
    }

	public static List<Button> GetAllCastingButtons() {
		return allCastingButtons;
	}

    public void CancelCasting() {
        Spell.StopCoroutines();
    }
}
