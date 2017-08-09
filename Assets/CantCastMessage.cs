using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CantCastMessage : MonoBehaviour {

	static CanvasRenderer cv;
	static Image image;
	public const float fadeTime = 3f;

	// Use this for initialization
	void Start () {
		cv = GetComponent<CanvasRenderer>();
		image = GetComponent<Image>();
		HideAnnouncement();
	}
	
	public static void ShowAnnouncement() {
		if (cv != null) {
			cv.gameObject.SetActive(true);
			image.CrossFadeAlpha(0f, fadeTime, false);
		} else {
			Debug.Log("LUImage ShowAnnouncement is bad");
		}
	}

	public static void HideAnnouncement() {
		cv.gameObject.SetActive(false);
	}
}
