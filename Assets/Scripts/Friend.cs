using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : MonoBehaviour {
	private const string friendFileName = "friends";
	private static Sprite[] sprites;

	void Start () {
		//https://answers.unity.com/questions/591677/how-to-get-child-sprites-from-a-multiple-sprite-te.html
		sprites = Resources.LoadAll<Sprite> (friendFileName);
		int index = Random.Range (0, sprites.Length);
		GetComponent<SpriteRenderer> ().sprite = sprites[index];
	}
}