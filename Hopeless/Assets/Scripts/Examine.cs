using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour {
	int counter = 0;
	int lifetime = 300;
	public static Examine activeExamine;
	// Use this for initialization
	void OnEnable () {
		if (!activeExamine) { 
			activeExamine = this;
		} else {
			activeExamine.gameObject.SetActive(false);
			activeExamine = this;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter += 1;
		if (counter > lifetime) {
			this.gameObject.SetActive (false);
			if (activeExamine == this) {
				activeExamine = null;
			}
		}
	}
}
