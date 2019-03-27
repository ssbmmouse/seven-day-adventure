using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour {
	public GameObject theRain;
	public static Rain rainController;
	public int[] rainyDays;
	// Use this for initialization
	void Start () {
		if (!rainController) {
			rainController = this;
		}
		SetRain ();
	}

	void OnEnable() {
		SetRain ();
	}
	// Update is called once per frame
	void Update () {
		
	}

	public void SetRain() {
		for (int i = 0; i < rainyDays.Length; i++) {
			if (Overworld.day == rainyDays [i]) {
				theRain.SetActive (true);
				break;
			} else {
				theRain.SetActive (false);
			}
		}
	}
}
