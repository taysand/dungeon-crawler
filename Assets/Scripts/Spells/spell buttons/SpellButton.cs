using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellButton : MonoBehaviour
{

    private Text label;
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
        }
    }

    protected abstract void InitializeName();

    public void CheckIfKnown()
    {
        if (Player.SpellIsKnown(spellName))
        {
            label.text = spellName + ": Learned!";
            GetComponent<Button>().interactable = false;
        }
        else
        {
            label.text = spellName;
        }
    }
}
