using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement code used in this class, Moving, Enemy, and Player borrowed from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/moving-object-script?playlist=17150

public class Game : MonoBehaviour
{
    private static List<Vector3> visibleSpots = new List<Vector3>();
    private static bool paused;
    private Player player;
    private static List<Enemy> enemies;
    private static bool playersTurn = true;
    private bool enemiesTurn;
    public float turnDelay = 0.1f;

    void Awake()
    {
        enemies = new List<Enemy>();
    }

    void Start()
    {
        Unpause();

        GameObject playerGameObj = GameObject.Find("Player");
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
        //check for opening and closing menus
        OpenLevelUp();
        //TODO: pause menu, map

        if (playersTurn || enemiesTurn || paused)
        {
            return;
        }

        StartCoroutine(MoveEnemies()); //something's messing up here
    }

    IEnumerator MoveEnemies() 
    {
		enemiesTurn = true;
		yield return new WaitForSeconds(turnDelay);
		if (enemies.Count == 0) 
        {
			yield return new WaitForSeconds(turnDelay);
		}

		for (int i = 0; i < enemies.Count; i++) 
        {
			enemies[i].Act();
			yield return new WaitForSeconds(enemies[i].moveTime);
		}

		playersTurn = true;
		enemiesTurn = false;
	}

    //TODO: check visible tiles, update visible tiles, update map
    //Game.UpdateMap();
    //Game.UpdateVisibility();

    private void OpenLevelUp()
    {
        if (Input.GetKeyDown(KeyCode.L) && LevelUp.Activated())
        {
            LevelUp.StaticHideLevelUpWindow();
            PlayAnimations(true);
        }
        else if (Input.GetKeyDown(KeyCode.L) && !LevelUp.Activated())
        {
            LevelUp.ShowLevelUpWindow();
            PlayAnimations(false);
        }
    }

    private void PlayAnimations(bool value)
    {
        player.GetComponent<Animator>().enabled = value;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Animator>().enabled = value;
        }
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

    public static bool IsPlayersTurn()
    {
        return playersTurn;
    }

    public static void SetPlayersTurn(bool value)
    {
        playersTurn = value;
    }

    public static void AddEnemyToList(Enemy badGuy)
    {
        enemies.Add(badGuy);
    }
}
