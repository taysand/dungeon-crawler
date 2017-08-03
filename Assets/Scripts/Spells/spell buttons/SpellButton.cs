using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellButton : MonoBehaviour
{

    private Text label;
    private Button button;
    protected string spellName;
    public const string levelUpButtonTag = "LevelUpButton";
    public const string castSpellButtonTag = "CastSpellButton";

    // Use this for initialization
    protected void Start()
    {
        // GameObject playerGameObj = GameObject.Find(Game.playerTag);
        // if (playerGameObj != null)
        // {
        //     playerGameObj.GetComponent<Player>().LearnSpell(Spell.drainSpell);
        // }
        // else
        // {
        //     Debug.Log("no player object?");
        // }
        button = GetComponent<Button>();
        InitializeName();
        if (tag == levelUpButtonTag)
        {
            InitializeName();
            label = GetComponentInChildren<Text>();
            CheckIfKnown();
        }
        else if (tag == castSpellButtonTag)
        {
            //TODO:display icon

            //replace this once the icons are up
            label = GetComponentInChildren<Text>();
            label.text = spellName;

            UpdateSpellSlots();
        }
    }

    protected abstract void InitializeName();

    public void CheckIfKnown()
    {
        if (Player.SpellIsKnown(spellName))
        {
            label.text = spellName + ": Learned!";
            button.interactable = false;
        }
        else
        {
            label.text = spellName;
        }
    }

    public void UpdateSpellSlots() {
        if (Player.SpellIsKnown(spellName))
        {
            label.text = spellName;
            button.interactable = true;
        }
        else
        {
            label.text = "Unknown";
            button.interactable = false;
        }
    }
}
