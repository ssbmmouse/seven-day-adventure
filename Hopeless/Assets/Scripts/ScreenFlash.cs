using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour {
	public static bool startFlash;
	public int duration;
	int counter;
	public GameObject flash;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (startFlash == true) {
			flash.SetActive (true);
			counter += 1;
			if (counter > duration) {
				flash.SetActive (false);
				counter = 0;
				startFlash = false;
			}
		}
	}
}
