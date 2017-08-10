﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    private CanvasRenderer cv;
    private Image image;

    public const string levelUpMessageName = "LevelUpMessage";
    public const string cantCastMessageName = "CantCastMessage";

	private float readTime = .5f;
	private float fadeRate = .03f;
	private float fadeDelay = .03f;

    void Start()
    {
        cv = GetComponent<CanvasRenderer>();
        image = GetComponent<Image>();
        HideMessage();
    }

    public void ShowMessage()
    {
        StartCoroutine(DisplayAndFadeMessage());
    }

    private IEnumerator DisplayAndFadeMessage()
    {
        if (cv != null)
        {
			cv.SetAlpha(1f);
			yield return new WaitForSeconds(readTime);
            while (true)
            {
                float alpha = cv.GetAlpha();
				if (alpha <= 0) {
					break;
				}
                cv.SetAlpha(alpha - fadeRate);
                yield return new WaitForSeconds(fadeDelay);
            }
            HideMessage();
        }
        else
        {
            Debug.Log("Message canvas renderer is gone?");
        }
    }

    private void HideMessage()
    {
        cv.SetAlpha(0f);
    }
}
