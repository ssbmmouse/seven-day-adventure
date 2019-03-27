using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAnim : MonoBehaviour {
	public float growthRate;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.localScale = new Vector3 (transform.localScale.x + growthRate, transform.localScale.y + growthRate, -2f);
		transform.position = new Vector3 (0, 0, -0.5f);
	}
}
