using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    private static GameObject canvas;
    private static bool activated;
    private static GameObject instructionsImage;

    private const string whileCasting = "WhileCasting";

    void Awake()
    {
        canvas = GetComponent<Canvas>().gameObject;
        ShowGameplayUI();

        instructionsImage = GameObject.Find(whileCasting);
        HideInstructions();
    }

    public static void HideInstructions()
    {
        Debug.Log("hiding instructions");
        instructionsImage.SetActive(false);
    }

    public static void ShowInstructions()
    {
        instructionsImage.SetActive(true);
    }

    public static bool Activated()
    {
        return activated;
    }

    public static void ShowGameplayUI()
    {
        activated = true;
        canvas.SetActive(activated);
    }

    public static void HideGameplayUI()
    {
        activated = false;
        canvas.SetActive(activated);
    }
}
