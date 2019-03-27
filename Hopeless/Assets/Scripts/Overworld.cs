using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overworld : MonoBehaviour {
	public int location;
	public static int day = 1;
	public static int timeOfDay;
	public GameObject[] options;
	public GameObject bgm;
	public TextMesh dayDisplay;
	public TextMesh timeOfDayDisplay;
	public GameObject[] examines;

	public bool isSanctuary;
	RaycastHit2D hit;
	// Use this for initialization
	void Start () {
		
	}

	void OnEnable() {
		OverworldPartyInfo.active = true;
		bgm.SetActive (true);
		if (isSanctuary) {
			Item.lastSanctuary = this.gameObject;
		}
	}

	void OnDisable() {
		OverworldPartyInfo.active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Move") {
					options [0].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Item") {
					options [1].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Destiny") {
					options [2].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Wait") {
					options [3].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Party 1") {
					PartyScreen.partyNum = 0;
					options [4].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Party 2") {
					PartyScreen.partyNum = 1;
					options [4].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Party 3") {
					PartyScreen.partyNum = 2;
					options [4].SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Party 4") {
					PartyScreen.partyNum = 3;
					options [4].SetActive (true);
					this.gameObject.SetActive (false);
				}
				for (int i = 0; i < examines.Length; i++) {
					if (hit.collider.name == "Examine (" + i.ToString () + ")") {
						examines [i].SetActive (true);
					}
				}
			}
		}
		if (timeOfDay >= 0 && timeOfDay < 3) { 
			timeOfDayDisplay.text = "Morning"; 
		}
		if (timeOfDay >= 3 && timeOfDay < 6) { 
			timeOfDayDisplay.text = "Day"; 
		}
		if (timeOfDay >= 6 && timeOfDay < 9) { 
			timeOfDayDisplay.text = "Night"; 
		}
		if (timeOfDay > 8) {
			timeOfDay = 0;
			day += 1;
		}
		dayDisplay.text = day.ToString();
	}
	public static void AdvanceTime() {
		timeOfDay += 1;
		if (timeOfDay > 8) {
			timeOfDay = 0;
			day += 1;
		}
	}
}
