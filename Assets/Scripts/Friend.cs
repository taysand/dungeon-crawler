using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//images
//https://www.shutterstock.com/image-vector/group-working-people-diversity-diverse-business-588269213
//https://www.shutterstock.com/image-vector/group-working-people-diversity-diverse-business-588269216
//https://www.shutterstock.com/image-vector/group-working-people-standing-on-white-390970807
//https://www.shutterstock.com/image-vector/group-working-people-standing-on-white-390970798?irgwc=1
public class Friend : MonoBehaviour {
	private const string friendFileName = "friends";
	private static Sprite[] sprites;

	void Start () {
		//https://answers.unity.com/questions/591677/how-to-get-child-sprites-from-a-multiple-sprite-te.html
		//https://answers.unity.com/questions/1114080/the-type-or-namespace-name-unityeditor-could-not-b-2.html
		sprites = Resources.LoadAll<Sprite> (friendFileName);
		int index = Random.Range (0, sprites.Length);
		GetComponent<SpriteRenderer> ().sprite = sprites[index];
	}
}