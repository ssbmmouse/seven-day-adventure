using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour {
	public Encounter[] timedEvents;
	public Encounter[] randomEvents;
	public GameObject overworld;
	public GameObject bgm;
	GameObject inst;
	bool encounterFound;
	int rng;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnEnable () {
		encounterFound = false;
		for (int i = 0; i < timedEvents.Length; i++) {
			if (timedEvents [i]) {
				if ((Overworld.day == timedEvents [i].dayToOccur) && (Overworld.timeOfDay / 3 == timedEvents [i].timeToOccur) && !timedEvents[i].encountered) {
					if (!timedEvents [i].mustBeFlagged) {
						inst = Instantiate (timedEvents [i].gameObject);
						inst.transform.parent = timedEvents [i].transform.parent;
						inst.transform.localScale = timedEvents [i].transform.localScale;
						inst.transform.position = timedEvents [i].transform.position;
						inst.gameObject.SetActive (true);
						timedEvents [i].encountered = true;
						encounterFound = true;
						break;
					} else if (timedEvents [i].mustBeFlagged) {
						if (Flags.flags [timedEvents [i].flag]) {
							inst = Instantiate (timedEvents [i].gameObject);
							inst.transform.parent = timedEvents [i].transform.parent;
							inst.transform.localScale = timedEvents [i].transform.localScale;
							inst.transform.position = timedEvents [i].transform.position;
							inst.gameObject.SetActive (true);
							timedEvents [i].encountered = true;
							encounterFound = true;
							break;
						}
					}
				}
			}
		}
		if (!encounterFound) {
			rng = Random.Range (0, 101);
			if (rng >= 90) { // Common
				ActivateEncounter(0);
			} else if (rng >= 80 && rng < 90) { // Common
				ActivateEncounter(1);
			} else if (rng >= 70 && rng < 80) { // Common
				ActivateEncounter(2);
			} else if (rng >= 60 && rng < 70) { // Common
				ActivateEncounter(3);
			} else if (rng >= 50 && rng < 60) { // Common
				ActivateEncounter(4);
			} else if (rng >= 40 && rng < 50) { // Common
				ActivateEncounter(5);
			} else if (rng >= 30 && rng < 40) { // Common
				ActivateEncounter(6);
			} else if (rng >= 20 && rng < 30) { // Common
				ActivateEncounter(7);
			} else if (rng >= 10 && rng < 20) { // Common
				ActivateEncounter(8);
			} else if (rng >= 5 && rng < 10) { // Rare
				ActivateEncounter(9);
			} else if (rng >= 2 && rng < 5) { // Super Rare
				ActivateEncounter(10);
			} else if (rng >= 0 && rng < 2) { // Ultra Rare
				ActivateEncounter(11);
			}
		}

		if (encounterFound) {
			this.gameObject.SetActive (false);
			bgm.SetActive (false);
		} else {
			overworld.SetActive(true);
			this.gameObject.SetActive (false);
		}

	}

	void ActivateEncounter (int slot) {
		if (randomEvents [slot]) {
			if (!randomEvents [slot].notTimeLimited) {
				if (randomEvents [slot].dayLimited) {
					if (Overworld.day == randomEvents [slot].dayToOccur && (Overworld.timeOfDay / 3) == randomEvents [slot].timeToOccur && !randomEvents [slot].encountered) {
						if (!randomEvents [slot].mustBeFlagged) {
							inst = Instantiate (randomEvents [slot].gameObject);
							inst.transform.parent = randomEvents [slot].transform.parent;
							inst.transform.localScale = randomEvents [slot].transform.localScale;
							inst.transform.position = randomEvents [slot].transform.position;
							inst.gameObject.SetActive (true);
							encounterFound = true;
							randomEvents [slot].encountered = true;
						} else {
							if (Flags.flags [randomEvents [slot].flag]) {
								inst = Instantiate (randomEvents [slot].gameObject);
								inst.transform.parent = randomEvents [slot].transform.parent;
								inst.transform.localScale = randomEvents [slot].transform.localScale;
								inst.transform.position = randomEvents [slot].transform.position;
								inst.gameObject.SetActive (true);
								encounterFound = true;
								randomEvents [slot].encountered = true;
							}
						}
					}
				} else {
					if (Overworld.day >= randomEvents [slot].dayToOccur && (Overworld.timeOfDay / 3) == randomEvents [slot].timeToOccur && !randomEvents [slot].encountered) {
						if (!randomEvents [slot].mustBeFlagged) {
							inst = Instantiate (randomEvents [slot].gameObject);
							inst.transform.parent = randomEvents [slot].transform.parent;
							inst.transform.localScale = randomEvents [slot].transform.localScale;
							inst.transform.position = randomEvents [slot].transform.position;
							inst.gameObject.SetActive (true);
							encounterFound = true;
							if (!randomEvents [slot].repeatable) {
								randomEvents [slot].encountered = true;
							}
						} else {
							if (Flags.flags [randomEvents [slot].flag]) {
								inst = Instantiate (randomEvents [slot].gameObject);
								inst.transform.parent = randomEvents [slot].transform.parent;
								inst.transform.localScale = randomEvents [slot].transform.localScale;
								inst.transform.position = randomEvents [slot].transform.position;
								inst.gameObject.SetActive (true);
								encounterFound = true;
								if (!randomEvents [slot].repeatable) {
									randomEvents [slot].encountered = true;
								}
							}
						}
					}
				}
			} else {
				if (!randomEvents [slot].encountered) {
					if (!randomEvents [slot].mustBeFlagged) {
						inst = Instantiate (randomEvents [slot].gameObject);
						inst.transform.parent = randomEvents [slot].transform.parent;
						inst.transform.localScale = randomEvents [slot].transform.localScale;
						inst.transform.position = randomEvents [slot].transform.position;
						inst.gameObject.SetActive (true);
						encounterFound = true;
						if (!randomEvents [slot].repeatable) {
							randomEvents [slot].encountered = true;
						}
					} else {
						if (Flags.flags [randomEvents [slot].flag]) {
							inst = Instantiate (randomEvents [slot].gameObject);
							inst.transform.parent = randomEvents [slot].transform.parent;
							inst.transform.localScale = randomEvents [slot].transform.localScale;
							inst.transform.position = randomEvents [slot].transform.position;
							inst.gameObject.SetActive (true);
							encounterFound = true;
							if (!randomEvents [slot].repeatable) {
								randomEvents [slot].encountered = true;
							}
						}
					}
				}
			}
			 
		}
	}
}
