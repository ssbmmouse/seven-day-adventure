using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundImage : MonoBehaviour { 	// Controls which background image to display, based on conditions
	SpriteRenderer r;							// Attach to background image gameobject
	public Sprite[] sprites;	// Set in editor, and is important they are in correct order
								// 0 = morning; 1 = day; 2 = night; 3 = morning (chaotic); 4 = day (chaotic); 5 = night (chaotic); 6 = rainy
	// Use this for initialization
	void Start () {
		r = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Overworld.day < 6) { // If the day is less than 6, different backgrounds are used.
			if (Overworld.timeOfDay < 3) { // If its morning...
				if (Overworld.day == 3 || Overworld.day == 5) { // If its day 3 or 5 (which use rainy backgrounds)
					r.sprite = sprites [6];	// set the rainy background (assign to sprites[6])
				} else {
					r.sprite = sprites [0]; // set the morning background
				}
			}
			if (Overworld.timeOfDay >= 3 && Overworld.timeOfDay < 6) { // If its day...
				if (Overworld.day == 3 || Overworld.day == 5) { // and if its day 3 or 5
					r.sprite = sprites [6];	// set rainy
				} else {
					r.sprite = sprites [1]; // set day
				}
			}
			if (Overworld.timeOfDay >= 6) { // if its night
				r.sprite = sprites [2]; // set night background (no rainy night background yet)
			}
		} else {  // if the day is 6 or greater, a new grimmer background is used
			if (Overworld.timeOfDay < 3) { // if morning
				r.sprite = sprites [3]; // set morning (chaotic)
			}
			if (Overworld.timeOfDay >= 3 && Overworld.timeOfDay < 6) { // if day
				r.sprite = sprites [4]; // set day (chaotic)
			}
			if (Overworld.timeOfDay >= 6) { // if night
				r.sprite = sprites [5]; // set night (chaotic)
			}
		}
	}
}
