using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LUText : MonoBehaviour {

	static CanvasRenderer cv;
	static Text text;

	// Use this for initialization
	void Start () {
		cv = GetComponent<CanvasRenderer>();
		text = GetComponent<Text>();
		HideAnnouncement();
	}

	public static void ShowAnnouncement() {
		if (cv != null) {//why, LUImage works fine
			cv.gameObject.SetActive(true);
			text.CrossFadeAlpha(0f, LUImage.fadeTime, false);
			Debug.Log("text ShowAnnouncement is good");
		} else {
			Debug.Log("text ShowAnnouncement is bad");
		}
	}

	public static void HideAnnouncement() {
		if (cv != null) {//why, LUImage works fine
			cv.gameObject.SetActive(false);
			Debug.Log("text HideAnnouncement is good");
		} else {
			Debug.Log("text HideAnnouncement is bad");
		}
	}
}
