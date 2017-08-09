using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SpellButton : MonoBehaviour
{

    protected Text label;
    protected Button button;
    protected Spell spell;
    protected Player player;
    protected Enemy enemy;
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
        spell = GetComponent<Spell>();

        GameObject playerGameObj = GameObject.Find(Game.playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        }

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

    public void UpdateSpellSlots()
    {
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

    protected IEnumerator WaitForClick()
    {
        //http://answers.unity3d.com/questions/904427/waiting-for-a-mouse-click-in-a-coroutine.html
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //https://forum.unity3d.com/threads/unity-2d-raycast-from-mouse-to-screen.211708/
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.transform.tag == Game.enemyTag)
                    {
                        enemy = hit.transform.gameObject.GetComponent<Enemy>();
                        yield break;
                    }
                }
            }

            yield return null;
        }
    }

    public IEnumerator TargetEnemy()
    {
        Game.Pause();

        yield return StartCoroutine(WaitForClick());

        bool success = spell.Cast(enemy);
        float healthLost = spell.GetHealthLost();
        if (success)
        {
            Debug.Log("spell cast");
            //https://stackoverflow.com/questions/3561202/check-if-instance-of-a-type
            if (!(spell.GetType() == typeof(DrainSpell)))
            {
                player.TakeDamage(healthLost);
            }
        }
        else
        {
            //TODO: some sort of message about how the enemy is too powerful
            //wait for a new click
            Message cantCastMessage = GameObject.Find(Message.cantCastMessageName).GetComponent<Message>();
            cantCastMessage.ShowMessage();
        }

        Game.Unpause();
    }

    public void OnClick()
    {
        StartCoroutine(TargetEnemy());
    }
}
