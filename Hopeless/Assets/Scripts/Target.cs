using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	public static bool enableTarget = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,0,0.5f));

		if (enableTarget) {
			transform.position = new Vector3 (Monster.playerTargeting.transform.position.x, Monster.playerTargeting.transform.position.y + 0.1f, 0);
		} else {
			transform.position = new Vector3 (0, 0, 200000);
		}
	}
}
