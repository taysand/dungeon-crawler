using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	protected CanvasRenderer cv;
	protected Image image;
	protected const float fadeTime = 1f;

	public const string levelUpMessageName = "LevelUpMessage";
	public const string cantCastMessageName = "CantCastMessage";

	void Start () {
		cv = GetComponent<CanvasRenderer>();
		image = GetComponent<Image>();
		HideMessage();
	}
	
	public void ShowMessage() {
		StartCoroutine(DisplayAndFadeMessage());
	}

	private IEnumerator DisplayAndFadeMessage() {
		if (cv != null) {
			cv.SetAlpha(1f);
			yield return new WaitForSeconds(fadeTime);
			HideMessage();
		} else {
			Debug.Log("Message canvas renderer is gone?");
		}
	}

	private void HideMessage() {
		cv.SetAlpha(0f);
	}
}
