using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving
{
    public float playerStartingHP = 100f;//should be constant
    public int playerStartingAC = 2;//should be constant
    public float playerStartingMaxHP = 100f;//should be constant
    public float playerStartingSpeed = 10f; //should be constant

    //leveling
    private int xp = 0;
    private int[] levels = { 100, 300, 600, 1000 }; //or whatever
    private int nextLevel;
    private int maxLevel;

    //conditions
    private bool hidden = false;

    //magic stuff
    private static List<string> knownSpells = new List<string>();
    private static List<string> unknownSpells = new List<string>();

    //private torch class probably 
    //does weak damage
    //can only illuminate 6 spaces
    //can't see through walls
    //need logic to make sure tiles on the other side of walls don't show up

    new void Start()
    {
        base.Start();
        nextLevel = levels[level];
        maxLevel = levels.Length;

        facingRight = true;

        unknownSpells = Spell.CreateSpellList();
    }

    protected override void SetStartingValues()
    {
        hp = playerStartingHP;
        ac = playerStartingAC;
        level = 0;
        maxHP = playerStartingMaxHP;
        speed = playerStartingSpeed;
    }

    public override void PlayAttackAnimation()
    {
        //TODO: player attack animation stuff
    }

    public void PlayInjuredAnimation()
    {
        animator.SetTrigger(Moving.playerInjuredAnimation);
    }

    new public void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        PlayInjuredAnimation();
        if (hp <= 0)
        {
            GameOver.ShowGameOver();
        }
    }

    void FixedUpdate()
    {
        //https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-character-controllers?playlist=17093
        var horizontal = Input.GetAxisRaw(Game.horizontalString);
        var vertical = Input.GetAxisRaw(Game.verticalString);
        rb2D.velocity = new Vector2(horizontal * speed, vertical * speed);

        if (facingRight && horizontal < 0)
        {
            Flip();
        }
        if (!facingRight && horizontal > 0)
        {
            Flip();
        }




        // if (Game.IsPlayersTurn() && !Game.IsPaused())
        // {
        //     int horizontal = 0;
        //     int vertical = 0;
        //     horizontal = (int)(Input.GetAxisRaw(Game.horizontalString));
        //     vertical = (int)(Input.GetAxisRaw(Game.verticalString));
        //     if (horizontal != 0)
        //     {
        //         vertical = 0;
        //     }
        //     if (horizontal != 0 || vertical != 0)
        //     {
        //         //TODO: this is broken
        //         if (facingRight && horizontal < 0) {
        //             Flip();
        //         }
        //         if (!facingRight && horizontal > 0) {
        //             Flip();
        //         }
        //         Move(horizontal, vertical);
        //         Game.SwitchTurns();
        //     }
        // }
    }

    public void CastSpell()
    {
        //TODO: please

        //use a spell
        //cast spell, pass the enemy the player's targeting 
        //if spell works, then health - spell.GetHealthLost
    }

    public void UsePotion()
    {
        //TODO: please
    }

    public void Heal(float amount)
    {
        if ((amount + hp) > maxHP)
        {
            hp = maxHP;
        }
        else
        {
            hp = hp + amount;
        }
    }

    public void IncreaseMaxHP(float amount)
    {
        maxHP = amount + maxHP;
        hp = amount + hp;
        DisplayPlayerHealth.UpdateHealthDisplay();
    }

    public void LevelUpM()
    {
        if (level < maxLevel)
        {
            level++;

            if (level < maxLevel)
            {
                nextLevel = levels[level];
            }

            LevelUp.GainLevelUpPoint();
            LUImage.ShowAnnouncement();
            LevelUp.ShowLevelUpWindow();
        }
    }

    public int GetExperience()
    {
        return xp;
    }

    public void IncreaseXP(int amount)
    {
        xp = xp + amount;
        if (xp >= nextLevel)
        {
            LevelUpM();
        }
    }

    public void Hide()
    {
        hidden = true;
    }

    public void NotHide()
    {
        hidden = false;
    }

    public bool Hidden()
    {
        return hidden;
    }

    public void IncreaseArmor(int amount)
    {
        ac = ac + amount;
    }

    public static List<string> GetKnownSpells()
    {
        return knownSpells;
    }

    public static List<string> GetUnknownSpells()
    {
        return unknownSpells;
    }

    public void LearnSpell(string spell)
    {
        knownSpells.Add(spell);
        unknownSpells.Remove(spell);
    }

    public static void ResetSpellList()
    {
        knownSpells = new List<string>();
    }

    public string GetHealthString()
    {
        return "HP: " + hp + "/" + maxHP;
    }

    public int GetNextLevelXP()
    {
        return nextLevel;
    }

    public static bool SpellIsKnown(string spellName)
    {
        return GetKnownSpells().Contains(spellName);
    }

    //to test leveling up. there's a button which should also be deleted
    public void TempLevelUpButton()
    {
        IncreaseXP(nextLevel - xp);
    }
}
