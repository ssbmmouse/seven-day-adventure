using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : MonoBehaviour {
	public static GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target != null) {
			transform.localScale = new Vector3 (transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, 1);
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, 0.25f);
		}
	}
}
