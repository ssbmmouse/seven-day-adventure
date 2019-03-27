using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalTextBoxSettings : MonoBehaviour {
	SpriteRenderer sprite;
	public static int boxColor;
	public static Sprite[] boxVarients = new Sprite[10];
	public Sprite[] awakeSprites;
	public bool init;
	public static bool inited;
	private int i;

	// Use this for initialization
	void Awake() {
		sprite = GetComponent<SpriteRenderer> ();
		if (init && !inited) {
			for (i = 1;i< boxVarients.Length; i++) {
				boxVarients [i] = awakeSprites [i];
			}
			boxVarients [0] = sprite.sprite;
			inited = true;
		}
	}

	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		sprite.sprite = boxVarients [boxColor];
	}
	
	// Update is called once per frame
	void Update () {
		sprite.sprite = boxVarients [boxColor];
	}
}
