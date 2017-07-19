using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving
{

    public float playerHP = 100f;//constants
    public int playerAC = 2;//constants 
    public float playerMaxHP = 100f; //constants 

    //movement stuff
    float maxSpeed = 10f;

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
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        nextLevel = levels[level];
        maxLevel = levels.Length - 1;

        unknownSpells = Spell.CreateSpellList();
    }

    protected override void SetStartingValues()
    {
        hp = playerHP;
        ac = playerAC;
        level = 0;
        maxHP = playerMaxHP;
    }

    public override void PlayAttackAnimation() {
        //TODO: player attack animation stuff
    }

    public void PlayInjuredAnimation() {
        animator.SetTrigger("PlayerInjured");
    }

    new public void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        PlayInjuredAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
        float move = Input.GetAxis("Horizontal");

        rb2D.velocity = new Vector2(move * maxSpeed, rb2D.velocity.y);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    new protected void Move()
    {
        base.Move();
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
        level++;
        if (level < 4)
        {
            nextLevel = levels[level];
        }
        LevelUp.GainLevelUpPoint();
        LUImage.ShowAnnouncement();
        LevelUp.ShowLevelUpWindow();
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

    public static bool IsKnown(string spellName)
    {
        return GetKnownSpells().Contains(spellName);
    }

    //to test leveling up. there's a button which should also be deleted
    public void TempLevelUpButton()
    {
        IncreaseXP(nextLevel - xp);
    }
}
