using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAnim : MonoBehaviour {
	public int[] timephases;
	int counter;

	// Use this for initialization
	void Start () {
		counter = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		counter += 1;
		if (counter > timephases [0] && counter < timephases[1]) {
			transform.localScale = new Vector3 (transform.localScale.x - 0.01f, transform.localScale.y - 0.01f, transform.localScale.z - 0.01f);
			transform.Rotate (transform.forward*19);
		} else if (counter > timephases [1]) {
			transform.localScale = new Vector3 (0.75f, 0.75f, 0.75f);
		}
	}
}
