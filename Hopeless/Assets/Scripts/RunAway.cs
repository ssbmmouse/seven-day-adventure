using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour {
	bool runaway;
	public TextMesh text;
	int speedValueParty;
	int speedValueEnemy;
	int counter;
	// Use this for initialization
	void OnEnable () {
		counter = 0;
		for (int i = 0; i < Party.party.Length; i++) {
			if (Party.party [i]) {
				speedValueParty += Party.party [i].GiveCurrentStat (5);
			}
		}

		for (int i = 0; i < Battle.activeMonsters.Length; i++) {
			if (Battle.activeMonsters [i]) {
				speedValueEnemy += Battle.activeMonsters [i].GiveCurrentStat (5);
			}
		}
		if (speedValueParty >= speedValueEnemy) {
			runaway = true;
			text.text = "Escaped!";
		} else if (Random.Range (0, 100) < (speedValueParty - speedValueEnemy) + 90) {
			runaway = true;
			text.text = "Escaped!";
		} else {
			runaway = false;
			text.text = "Failed to run away...";
		}
	}
	
	// Update is called once per frame
	void Update () {
		counter += 1;
		if (Input.GetMouseButton (0) && counter > 10) {
			if (!runaway) {
				this.gameObject.SetActive (false);
				Battle.battling = true;
				Battle.ranAway = false;
			} else {
				this.gameObject.SetActive (false);
				Battle.ranAway = true;
				Battle.battling = true;
			}
		}
	}
}
