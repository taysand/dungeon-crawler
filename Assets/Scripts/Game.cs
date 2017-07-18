using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

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
	private static bool paused = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//maybe change to GetButton
		if (Input.GetKeyDown(KeyCode.L) && LevelUp.Activated()) { 
			LevelUp.StaticHideLevelUpWindow();
		} 
		else if (Input.GetKeyDown(KeyCode.L) && !LevelUp.Activated()) {
			LevelUp.ShowLevelUpWindow();
		}
	
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
	}

	public static List<Vector3> GetVisibleSpots() {
		return visibleSpots;
	}

	public static bool IsPaused() {
        return paused;
    }

	public static void Pause() {
		paused = true;
	}

	public static void Unpause() {
		paused = false;
	}
}
