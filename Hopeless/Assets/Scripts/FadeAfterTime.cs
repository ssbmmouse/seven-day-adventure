using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAfterTime : MonoBehaviour {
	public int time;
	int counter;
	// Use this for initialization
	void OnEnable () {
		counter = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter += 1;
		if (counter > time) {
			this.gameObject.SetActive (false);
		}
	}
	void Update() {

	}
}
