﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    private static bool paused;
    private static bool animationsPaused;
    private Player player;
    private static List<Enemy> enemies;

    //movement
    public float turnDelay = .1f;
    private bool enemiesMoving;
    private static bool playersTurn = true;

    //tag strings
    public const string playerTag = "Player";
    public const string wallTag = "Wall";
    public const string enemyTag = "Enemy";
    public const string torchTag = "torch";

    //input names
    public const string horizontalString = "Horizontal";
    public const string verticalString = "Vertical";
    private const string pauseKey = "Pause";
    private const string levelKey = "Open Level";

    //menus
    public Menu levelUpMenu;
    public Menu pauseMenu;

    //story stuff
    public Canvas storyCanvas;
    public Canvas gameplayCanvas;
    public Text storyText;
    private const string firstStory = "You wake up in a cave surrounded by treasure. There are heavy footsteps coming from the darkness ahead of you. What's going on? You grab the torch next to you and plan your escape.";
    private const string secondStory = "second story text"; //TODO:
    public static bool toNextStory = true;
    private string[] storyArray = { firstStory, secondStory };
    private int storyIndex = 0;
    private bool firstLevel = true;

    //camera zoom
    private Camera mainCamera;
    private float cameraIn = 30;
    private float cameraOut = 70;
    private bool zoomingOut = false;
    private bool doneZooming = false;

    void Awake () {
        mainCamera = GetComponent<Camera> ();
        enemies = new List<Enemy> ();
        Spell.BuildSpellNameList ();
        GameObject playerGameObj = GameObject.Find (playerTag);
        if (playerGameObj != null) {
            player = playerGameObj.GetComponent<Player> ();
        } else {
            Debug.Log ("no player object?");
        }
        animationsPaused = false;
    }

    #region levels
    public void LevelSetup (string levelText) {
        Pause ();
        PlayAnimations (false);
        gameplayCanvas.gameObject.SetActive (false);
        storyCanvas.gameObject.SetActive (true);
        mainCamera.fieldOfView = cameraIn;
        storyText.text = levelText;
    }

    public void StartLevel () {
        gameplayCanvas.gameObject.SetActive (true);
        storyCanvas.gameObject.SetActive (false);
        StartCoroutine (CameraZoomOut ());
    }

    public void ShowLevelUpInstructions() {
        Message levelUpInstructions = GameObject.Find(Message.levelUpInstructionsMessageName).GetComponent<Message>();
        levelUpInstructions.ShowMessage(2.6f, .04f, .03f);
        levelUpMenu.ShowMenu ();
    }

    private IEnumerator CameraZoomOut () {
        zoomingOut = true;
        yield return new WaitForSeconds (.5f);
        while (mainCamera.fieldOfView < cameraOut) {
            mainCamera.fieldOfView += 2;
            yield return new WaitForSeconds (.05f);
        }
        doneZooming = true;
        Unpause ();
        if (!firstLevel) {
            player.LevelUp ();
        } else {
            firstLevel = false;
            ShowLevelUpInstructions();
            Debug.Log ("first level has started now");
        }
    }
    #endregion //levels

    #region check input
    void Update () {
        if (toNextStory) {
            toNextStory = false;
            LevelSetup (storyArray[storyIndex++]);
        }

        if (pauseMenu.Activated ()) {
            CheckPauseWindow ();
        } else if (levelUpMenu.Activated ()) {
            CheckLevelUpWindow ();
        } else {
            CheckPauseWindow ();
            CheckLevelUpWindow ();
        }

        ControlAnimations ();

        if (playersTurn || enemiesMoving || paused) {
            return;
        }

        // Debug.Log("enemies' turn");
        StartCoroutine (MoveEnemies ());
    }

    private void CheckLevelUpWindow () {
        if (Input.GetButtonUp (levelKey) && levelUpMenu.Activated ()) {
            levelUpMenu.HideMenu ();
            PlayAnimations (true);
        } else if (Input.GetButtonUp (levelKey) && !levelUpMenu.Activated ()) {
            levelUpMenu.ShowMenu ();
            PlayAnimations (false);
        }
    }

    private void CheckPauseWindow () {
        if (Input.GetButtonUp (pauseKey) && !paused && !Spell.Casting ()) {
            pauseMenu.ShowMenu ();
            Debug.Log ("should show pause");
            PlayAnimations (true);
        } else if (Input.GetButtonUp (pauseKey) && paused) {
            pauseMenu.HideMenu ();
            PlayAnimations (false);
        }
    }
    #endregion //check input

    #region animations
    private void ControlAnimations () {
        // Debug.Log("paused is " + paused + " and animationsPaused is " + animationsPaused);
        if (zoomingOut) {
            // Debug.Log("zooming out");
            PlayAnimations (true);
            zoomingOut = false;
        } else if (doneZooming) {
            // Debug.Log("done zooming");
            if (paused && !animationsPaused) {
                // Debug.Log("no more animation");
                PlayAnimations (false);
            } else if (!paused && animationsPaused) {
                // Debug.Log("yes more animation");
                PlayAnimations (true);
            }
        }
    }

    private void PlayAnimations (bool value) {
        player.GetComponent<Animator> ().enabled = value;

        //enemies
        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].GetComponent<Animator> ().enabled = value;
        }

        //torches
        GameObject[] torches = GameObject.FindGameObjectsWithTag (torchTag);
        foreach (GameObject torch in torches) {
            torch.GetComponent<Animator> ().enabled = value;
        }

        animationsPaused = !value;
    }
    #endregion //animations

    #region control
    public static bool IsPaused () {
        return paused;
    }

    public static void Pause () {
        paused = true;
    }

    public static void Unpause () {
        paused = false;
    }

    public static bool IsPlayersTurn () {
        return playersTurn;
    }

    public static void SetPlayersTurn (bool value) {
        playersTurn = value;
    }
    #endregion //control

    #region enemies
    IEnumerator MoveEnemies () {
        enemiesMoving = true;
        if (enemies.Count == 0) {
            yield return new WaitForSeconds (turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].MoveEnemy ();
            yield return new WaitForSeconds (enemies[i].moveTime);
        }
        yield return new WaitForSeconds (turnDelay);

        playersTurn = true;
        enemiesMoving = false;
    }

    public static void AddEnemyToList (Enemy badGuy) {
        enemies.Add (badGuy);
        // Debug.Log("enemies list looks like this right now: ");
        // for (int i = 0; i < enemies.Count; i++) {
        //     Debug.Log(enemies[i]);
        // }
    }
    #endregion //enemies
}