using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : MonoBehaviour {
	RaycastHit2D hit;
	public GameObject randomEncounter;
	public GameObject overworld;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Short") {
					Overworld.AdvanceTime ();
					randomEncounter.SetActive (true);
					Rain.rainController.SetRain ();
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Long") {
					Overworld.AdvanceTime ();
					Overworld.AdvanceTime ();
					Overworld.AdvanceTime ();
					randomEncounter.SetActive (true);
					Rain.rainController.SetRain ();
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Cancel") {
					overworld.SetActive (true);
					this.gameObject.SetActive (false);
				}
			}
		}
	}
}
