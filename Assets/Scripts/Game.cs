using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement code used in this class, Moving, Enemy, and Player borrowed from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/moving-object-script?playlist=17150

public class Game : MonoBehaviour
{

    //display enemy health
    //display enemy levels
    //display player health
    //display map
    //open map
    //control turns
    //pause game
    //checks if enemies are sleeping or frozen, then they can't move
    //keeps track of scared enemies
    //moves enemies

    private static List<Vector3> visibleSpots = new List<Vector3>();
    private static bool paused;
    private Player player;
    private static List<Enemy> enemies;
    private static bool playersTurn = true;
    private bool enemiesTurn;
    public float turnDelay = 0.1f;

	void Awake() {
		 enemies = new List<Enemy>();
	}

    // Use this for initialization
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

    // Update is called once per frame
    void Update()
    {
        //check for opening and closing menus
        OpenLevelUp();

        if (playersTurn || enemiesTurn || paused)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
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

    //ugh
    // //don't do anything else if it's paused
    // if (IsPaused())
    // {
    //     return;
    // }

    // if (IsPlayersTurn())
    // {
    //     //actually needs to check for casting spells, movement, or other actions
    //     
    // }

    // if (IsEnemiesTurn())
    // {

    //     int count = enemies.Count;
    // }

    //don't know if this works
    //Game.CheckTiles();
    //input
    //player action 
    //movement
    //spell
    //heal
    //access map, pauses game
    //get level up window, pauses game
    //get craft window, pauses game
    // if (!Game.IsPaused) {
    //call enemy actions for every enemy in the room
    //separate class for each room/level?
    // }
    // Game.UpdateVisibility(); //tiles, enemies
    // Game.UpdateMap();
    // }

    private void OpenLevelUp()
    {
        if (Input.GetKeyDown(KeyCode.L) && LevelUp.Activated())
        {
            LevelUp.StaticHideLevelUpWindow();
        }
        else if (Input.GetKeyDown(KeyCode.L) && !LevelUp.Activated())
        {
            LevelUp.ShowLevelUpWindow();
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

    public bool IsEnemiesTurn()
    {
        return enemiesTurn;
    }

    public static void AddEnemyToList(Enemy badGuy)
    {
        enemies.Add(badGuy);
    }
}
