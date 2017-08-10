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

    private static bool casting;

    public const string levelUpButtonTag = "LevelUpButton";
    private const string castSpellButtonTag = "CastSpellButton";
    private const string targetEnemy = "TargetEnemy";
    private const string waitForClick = "WaitForClick";

    // Use this for initialization
    protected void Start()
    {
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

        casting = false;
    }

    public static bool Casting()
    {
        return casting;
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
        casting = true;
        Game.Pause();
        GameplayUI.ShowInstructions();

        bool success = false;

        while (true)
        {
            yield return StartCoroutine(waitForClick);

            success = spell.Cast(enemy);

            if (success)
            {
                float healthLost = spell.GetHealthLost();

                Debug.Log("spell cast");

                //https://stackoverflow.com/questions/3561202/check-if-instance-of-a-type
                if (!(spell.GetType() == typeof(DrainSpell)))
                {
                    player.TakeDamage(healthLost);
                }
                break;
            }
            else
            {
                Message cantCastMessage = GameObject.Find(Message.cantCastMessageName).GetComponent<Message>();
                cantCastMessage.ShowMessage();
            }
        }
        End();
    }

    public void OnCastingClick()
    {
        StartCoroutine(targetEnemy);
    }

    private void End()
    {
        Game.Unpause();
        casting = false;
        GameplayUI.HideInstructions();

    }

    public void CancelCasting()
    {
        StopCoroutine(waitForClick);
        StopCoroutine(targetEnemy);
        End();
    }
}
