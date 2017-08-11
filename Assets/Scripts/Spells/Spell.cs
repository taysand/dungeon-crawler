using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{

    //spell stats
    protected string spellName;
    protected float healthLost;
    protected int maxLevelAffected;
    protected int level = 0;

    //spell name constants
    public const string delevelSpell = "Reduce Level";
    public const string drainSpell = "HP Drain";
    public const string freezeSpell = "Freeze";
    public const string scareSpell = "Scare";
    public const string teleportSpell = "Teleport";
    public const string transformSpell = "Transform";
    public const string sleepSpell = "Sleep";

    //coroutine names
    private const string targetEnemy = "TargetEnemy";
    private const string waitForClick = "WaitForClick";

    //casting stuff
    private static bool casting = false;
    private Enemy enemy;
    protected static Spell spell;

    private static List<string> allSpellNames = new List<string>();

    void Awake()
    {
        InitializeStats();
        spell = GetComponent<Spell>();
    }

    protected abstract void InitializeStats();

    public static void BuildSpellNameList()
    {
        allSpellNames.Add(delevelSpell);
        allSpellNames.Add(drainSpell);
        allSpellNames.Add(freezeSpell);
        allSpellNames.Add(scareSpell);
        allSpellNames.Add(teleportSpell);
        allSpellNames.Add(transformSpell);
        allSpellNames.Add(sleepSpell);
    }

    public abstract bool Cast(Enemy enemy);

    public float GetHealthLost()
    {
        return healthLost;
    }

    public string GetSpellName()
    {
        return spellName;
    }

    protected virtual void LevelUpSpell()
    {
        level++;
        //TODO: 
        //options to increase maxLevelAffected, decrease healthLost
        //a cap on the number of times you can level up a spell
    }

    public static List<string> GetAllSpellNames()
    {
        return allSpellNames;
    }

    public static bool Casting()
    {
        return casting;
    }

    public void OnCastingClick()
    {
        StartCoroutine(targetEnemy);
    }

    public void OnLevelUpClick(Button button)
    {
        LevelUp.SpendLevelUpPoints();
        Player.LearnSpell(spellName);
        SpellButtons.CheckIfKnown(button);

        int buttonIndex = allSpellNames.IndexOf(spellName);
        Button castingButton = SpellButtons.GetAllCastingButtons()[buttonIndex];
        SpellButtons.UpdateSpellSlots(castingButton);

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

            success = Cast(enemy);

            if (success)
            {
                float healthLost = GetHealthLost();

                Debug.Log("spell cast");

                //https://stackoverflow.com/questions/3561202/check-if-instance-of-a-type
                if (!(GetType() == typeof(DrainSpell)))
                {
                    GameObject playerGameObj = GameObject.Find(Game.playerTag);
                    if (playerGameObj != null)
                    {
                        playerGameObj.GetComponent<Player>().TakeDamage(healthLost);
                    }
                    else
                    {
                        Debug.Log("no player object?");
                    }
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

    private void End()
    {
        Game.Unpause();
        casting = false;
        GameplayUI.HideInstructions();

    }

    public static void StopCoroutines()
    {
        spell.StopCoroutine(waitForClick);
        spell.StopCoroutine(targetEnemy);
        spell.End();
    }
}
