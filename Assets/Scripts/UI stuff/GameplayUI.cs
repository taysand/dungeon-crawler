using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{

    static GameObject gm;
    static bool activated;

    // Use this for initialization
    void Start()
    {
        gm = GetComponent<Canvas>().gameObject;
        ShowGameplayUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static bool Activated()
    {
        return activated;
    }

    public static void ShowGameplayUI()
    {
        activated = true;
        gm.SetActive(activated);
    }

    public static void HideGameplayUI()
    {
        activated = false;
        gm.SetActive(activated);
    }
}
