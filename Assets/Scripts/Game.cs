using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static List<Vector3> visibleSpots = new List<Vector3>();
    private static bool paused;
    private static bool animationsPaused;
    private Player player;
    private static List<Enemy> enemies;
    //private static bool playersTurn = true;
    // private static bool enemiesTurn;
    // public float turnDelay = 0.1f;

    public const string playerTag = "Player";
    public const string wallTag = "Wall";
    public const string enemyTag = "Enemy";

    //input names
    public const string horizontalString = "Horizontal";
    public const string verticalString = "Vertical";
    private const string pauseKey = "Pause";
    private const string levelKey = "Open Level";

    //menus
    public Menu levelUpMenu;
    public Menu pauseMenu;

    //public float fixedUpdateTime = .4f;

    void Awake()
    {
        enemies = new List<Enemy>();
        Spell.BuildSpellNameList();
        //Time.fixedDeltaTime = fixedUpdateTime;
    }

    void Start()
    {
        Unpause();
        animationsPaused = false;

        GameObject playerGameObj = GameObject.Find(playerTag);
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
        else
        {
            Debug.Log("no player object?");
        }
    }

    // void Update()
    // {
    //     //check for opening and closing menus, but only if a menu isn't already open
    //     //TODO: map
    //     if (pauseMenu.Activated())
    //     {
    //         CheckPauseWindow();
    //     }
    //     else if (levelUpMenu.Activated())
    //     {
    //         CheckLevelUpWindow();
    //     }
    //     else
    //     {
    //         CheckPauseWindow();
    //         CheckLevelUpWindow();
    //     }

    //     ControlAnimations();
    // }

    // void FixedUpdate()
    // {
    //     if (!paused)
    //     {
    //         if (playersTurn)
    //         {
    //             return;
    //         }

    //         if (enemiesTurn)
    //         {
    //             for (int i = 0; i < enemies.Count; i++)
    //             {
    //                 enemies[i].Act();
    //             }
    //             SwitchTurns();
    //         }
    //     }
    // }

    // private void ControlAnimations()
    // {
    //     if (paused && !animationsPaused)
    //     {
    //         PlayAnimations(false);
    //     }
    //     else if (!paused && animationsPaused)
    //     {
    //         PlayAnimations(true);
    //     }
    // }
    //TODO: check visible tiles, update visible tiles, update map
    //Game.UpdateMap();
    //Game.UpdateVisibility();

    private void CheckLevelUpWindow()
    {
        if (Input.GetButtonUp(levelKey) && levelUpMenu.Activated())
        {
            levelUpMenu.HideMenu();
            //PlayAnimations(true);
        }
        else if (Input.GetButtonUp(levelKey) && !levelUpMenu.Activated())
        {
            levelUpMenu.ShowMenu();
            //PlayAnimations(false);
        }
    }

    private void CheckPauseWindow()
    {
        if (Input.GetButtonUp(pauseKey) && !paused && !Spell.Casting())
        {
            pauseMenu.ShowMenu();
            Debug.Log("should show pause");
            //PlayAnimations(true);
        }
        else if (Input.GetButtonUp(pauseKey) && paused)
        {
            pauseMenu.HideMenu();
            //PlayAnimations(false);
        }
    }

    // private void PlayAnimations(bool value)
    // {
    //     player.GetComponent<Animator>().enabled = value;
    //     for (int i = 0; i < enemies.Count; i++)
    //     {
    //         Enemy enemy = enemies[i];
    //         enemy.GetComponent<Animator>().enabled = value;
    //         if (value == true)
    //         {
    //             if (enemy.ShouldMove())
    //             {
    //                 enemy.StartMovement();
    //             }
    //         }
    //         else
    //         {
    //             enemy.StopMovement();
    //         }
    //     }
    //     animationsPaused = !value;
    // }

    public static List<Vector3> GetVisibleSpots()
    {
        return visibleSpots;
    }

    public static bool IsPaused()
    {
        return paused;
    }

    public static void Pause()
    {
        paused = true;
    }

    public static void Unpause()
    {
        paused = false;
    }

    // public static bool IsPlayersTurn()
    // {
    //     return playersTurn;
    // }

    // public static void SwitchTurns()
    // {
    //     playersTurn = !playersTurn;
    //     enemiesTurn = !enemiesTurn;
    // }

    public static void AddEnemyToList(Enemy badGuy)
    {
        enemies.Add(badGuy);
        // Debug.Log("enemies list looks like this right now: ");
        // for (int i = 0; i < enemies.Count; i++) {
        //     Debug.Log(enemies[i]);
        // }
    }

    //FIXME: attempting turn based below
    public float turnDelay = .1f;
    private bool enemiesMoving;

    private const float playerTurnDelay = .5f;

    private static bool playersTurn = true;

    public static bool IsPlayersTurn()
    {
        return playersTurn;
    }

    public static void SetPlayersTurn(bool value)
    {
        playersTurn = value;
    }

    //https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial
    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        yield return new WaitForSeconds(turnDelay);

        // StartCoroutine(WaitBeforePlayerCanMoveAgain());
        playersTurn = true;
        enemiesMoving = false;
    }

    // IEnumerator WaitBeforePlayerCanMoveAgain()
    // {
    //     // Debug.Log("waiting for player's turn");
    //     yield return new WaitForSeconds(playerTurnDelay);
    //     // Debug.Log("setting player's turn to go");
    //     playersTurn = true;
    //     // Debug.Log("player's turn should be true and is: " + playersTurn);
    // }

    //https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial
    void Update()
    {
        if (pauseMenu.Activated())
        {
            CheckPauseWindow();
        }
        else if (levelUpMenu.Activated())
        {
            CheckLevelUpWindow();
        }
        else
        {
            CheckPauseWindow();
            CheckLevelUpWindow();
        }

        // ControlAnimations();//TODO: does the roguelike code deal with this

        if (playersTurn || enemiesMoving)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }
}
