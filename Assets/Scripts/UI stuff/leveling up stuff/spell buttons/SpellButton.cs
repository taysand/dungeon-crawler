using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellButton : MonoBehaviour
{

    private Text label;
    protected string spellName;

    // Use this for initialization
    protected void Start()
    {
        GameObject playerGameObj = GameObject.Find("Player");
        if (playerGameObj != null)
        {
            playerGameObj.GetComponent<Player>().LearnSpell(Spell.drainSpell);
        }
        else
        {
            Debug.Log("no player object?");
        }

        InitializeName();
        label = GetComponentInChildren<Text>();
        CheckIfKnown();
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
