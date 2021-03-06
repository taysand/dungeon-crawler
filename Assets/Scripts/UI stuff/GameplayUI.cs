﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//layout
//https://answers.unity.com/questions/1190977/unity-gui-splitting-vertical-layout-group-by-ratio.html
//https://stackoverflow.com/questions/39529789/layout-group-components-with-special-division
public class GameplayUI : MonoBehaviour {
    private static GameObject canvas;
    private static bool activated;
    private static GameObject instructionsImage;

    private const string whileCasting = "WhileCasting";

    void Awake () {
        canvas = GetComponent<Canvas> ().gameObject;
        ShowGameplayUI ();

        instructionsImage = GameObject.Find (whileCasting);
        HideInstructions ();
    }

    public static void HideInstructions () {
        instructionsImage.SetActive (false);
    }

    public static void ShowInstructions () {
        instructionsImage.SetActive (true);
    }

    public static bool Activated () {
        return activated;
    }

    public static void ShowGameplayUI () {
        activated = true;
        canvas.SetActive (activated);
    }

    public static void HideGameplayUI () {
        activated = false;
        canvas.SetActive (activated);
    }
}