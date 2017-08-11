﻿using System.Collections;
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

    void Update()
    {
        //check for opening and closing menus, but only if a menu isn't already open
        //TODO: map
        if (PauseWindow.Activated())
        {
            CheckPauseWindow();
        }
        else if (LevelUp.Activated())
        {
            CheckLevelUpWindow();
        }
        else
        {
            CheckPauseWindow();
            CheckLevelUpWindow();
        }

        ControlAnimations();
    }

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

    private void ControlAnimations()
    {
        if (paused && !animationsPaused)
        {
            PlayAnimations(false);
        }
        else if (!paused && animationsPaused)
        {
            PlayAnimations(true);
        }
    }
    //TODO: check visible tiles, update visible tiles, update map
    //Game.UpdateMap();
    //Game.UpdateVisibility();

    private void CheckLevelUpWindow()
    {
        if (Input.GetButtonUp(levelKey) && LevelUp.Activated())
        {
            LevelUp.StaticHideLevelUpWindow();
            //PlayAnimations(true);
        }
        else if (Input.GetButtonUp(levelKey) && !LevelUp.Activated())
        {
            LevelUp.ShowLevelUpWindow();
            //PlayAnimations(false);
        }
    }

    private void CheckPauseWindow()
    {
        if (Input.GetButtonUp(pauseKey) && !paused && !Spell.Casting())
        {
            PauseWindow.ShowPauseWindow();
            //PlayAnimations(true);
        }
        else if (Input.GetButtonUp(pauseKey) && paused)
        {
            PauseWindow.HidePauseWindow();
            //PlayAnimations(false);
        }
    }

    private void PlayAnimations(bool value)
    {
        player.GetComponent<Animator>().enabled = value;
        for (int i = 0; i < enemies.Count; i++)
        {
            Enemy enemy = enemies[i];
            enemy.GetComponent<Animator>().enabled = value;
            if (value == true)
            {
                enemy.StartMovement();
            }
            else
            {
                enemy.StopMovement();
            }
        }
        animationsPaused = !value;
    }

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
    }
}
