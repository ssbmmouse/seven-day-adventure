using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
	RaycastHit2D hit;
	public GameObject[] nearbyLocations;
	public GameObject theOverworld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Loc1") {
					nearbyLocations [0].SetActive (true);
					theOverworld.SetActive (true);
					transform.parent.gameObject.SetActive (false);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Loc2") {
					nearbyLocations [1].SetActive (true);
					theOverworld.SetActive (true);
					transform.parent.gameObject.SetActive (false);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Loc3") {
					nearbyLocations [2].SetActive (true);
					theOverworld.SetActive (true);
					transform.parent.gameObject.SetActive (false);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Cancel") {
					theOverworld.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.tag == "Timed") {
					Overworld.timeOfDay += 1;
				}
			}
		}
	}
}
