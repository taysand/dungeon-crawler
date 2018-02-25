using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Friend : MonoBehaviour {

	private static Object[] sprites;

	// Use this for initialization
	void Start () {
		// sprites = AssetDatabase.LoadAllAssetsAtPath("friends");

		sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/Sprites/friends.png");
		// Debug.Log(sprites.Length);
		// Debug.Log(sprites[2].name);
		int index = Random.Range(0, sprites.Length);
		GetComponent<SpriteRenderer>().sprite = (Sprite) sprites[index];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
