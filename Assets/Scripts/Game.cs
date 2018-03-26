using System.Collections;
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
    private const string secondStory = "You barely have time to catch your breath after that narrow escape before something pale charges you. As it does, you feel a rush of power and realize there are sparks crackling at your fingertips.";
    private const string thirdStory = "Empowered, you venture further, believing in your ability to escape this awful place.";
    private const string fourthStory = "The last room was tough, but you know the only way to survive is to keep moving forward.";
    private const string endStory = "The door creaks open. You and your new friends step out into the sunlight.";
    public static bool toNextStory = true;
    public static bool beatGame = false;
    private string[] storyArray = { firstStory, secondStory, thirdStory, fourthStory };
    private int storyIndex = 0;
    private bool firstLevel = true;

    //camera zoom
    private Camera mainCamera;
    private float cameraIn = 30;
    private float cameraOut = 70;
    private bool zoomingOut = false;
    private bool doneZooming = false;

    //winning
    public static int requiredFriendsToWin = 2;

    //messages
    private string levelUpInstructionsText = "Press L to show and hide the level up window";
    private float levelUpInstructionsReadTime = 2.6f;
    private float levelUpInstructionsFadeRate = .04f;
    private float levelUpInstructionsFadeDelay = .03f;
    
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
        if (levelText == endStory) {
            Button progressButton = storyCanvas.gameObject.GetComponentInChildren<Button>();
            Text progressText = progressButton.GetComponentInChildren<Text>();
            progressText.text = "You win! This button does nothing.";
            progressText.resizeTextForBestFit = true;
            
        }
    }

    public void StartLevel () {
        gameplayCanvas.gameObject.SetActive (true);
        storyCanvas.gameObject.SetActive (false);
        StartCoroutine (CameraZoomOut ());
    }

    public void ShowLevelUpInstructions () {
        Message.SetAndDisplayMessage(levelUpInstructionsReadTime, levelUpInstructionsFadeRate, levelUpInstructionsFadeDelay, levelUpInstructionsText);
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
            player.IncreaseXP (player.GetNextLevel() - player.GetExperience());
        } else {
            firstLevel = false;
            ShowLevelUpInstructions ();
        }
    }
    #endregion //levels

    #region check input
    void Update () {
        if (beatGame) {
            LevelSetup (endStory);
            //show end menu? quit game? pause? restart?
        }

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
            PlayAnimations (true);
        } else if (Input.GetButtonUp (pauseKey) && paused) {
            pauseMenu.HideMenu ();
            PlayAnimations (false);
        }
    }
    #endregion //check input

    #region animations
    private void ControlAnimations () {
        if (zoomingOut) {
            PlayAnimations (true);
            zoomingOut = false;
        } else if (doneZooming) {
            if (paused && !animationsPaused) {
                PlayAnimations (false);
            } else if (!paused && animationsPaused) {
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

        yield return new WaitForSeconds (turnDelay);

        if (enemies.Count == 0) {
            yield return new WaitForSeconds (turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++) {
            enemies[i].MoveEnemy ();
        }
        yield return new WaitForSeconds (turnDelay);

        playersTurn = true;
        enemiesMoving = false;
    }

    public static void AddEnemyToList (Enemy badGuy) {
        enemies.Add (badGuy);
    }
    #endregion //enemies
}