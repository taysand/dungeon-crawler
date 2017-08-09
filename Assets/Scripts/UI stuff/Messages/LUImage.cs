using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LUImage : MonoBehaviour {

	static CanvasRenderer cv;
	static Image image;
	public const float fadeTime = 3f;
	static LUImage instance;

	// Use this for initialization
	void Start () {
		cv = GetComponent<CanvasRenderer>();
		image = GetComponent<Image>();
		instance = GetComponent<LUImage>();
		HideAnnouncement();
	}
	
	public static void ShowAnnouncement() {
		instance.StartCoroutine(DisplayMessage());
	}

	private static IEnumerator DisplayMessage() {
		if (cv != null) {
			cv.SetAlpha(1f);
			yield return new WaitForSeconds(fadeTime);
			HideAnnouncement();
		} else {
			Debug.Log("LUImage ShowAnnouncement is bad");
		}
	}

	public static void HideAnnouncement() {
		cv.SetAlpha(0f);
	}
}
