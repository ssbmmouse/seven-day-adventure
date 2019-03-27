using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEvent : MonoBehaviour {
	public bool isChoice;
	public int outcome;
	public GameObject nextEvent;
	public int destinyToGive;
	RaycastHit2D hit;

	public Monster monsterToAdd;
	public GameObject[] choiceOutcomes;

	public int charismaCheck;
	public GameObject failEvent;

	public Item rewardItem;
	public int rewardItemAmount;

	public GameObject[] objects;
	public string monToRemove;

	public int[] alignmentShift = new int[3];

	public bool checksEssence;
	// Enable gameobject to activate
	void OnEnable () {
		if (outcome == 12) {
			objects[0].GetComponent<TextMesh> ().text = objects[0].GetComponent<TextMesh> ().text.Replace ("[ESSENCE]", (rewardItemAmount * Party.party [0].statsMax [7]).ToString ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isChoice) {
			if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.R) || (Input.GetMouseButtonDown (0))) {
				if (outcome == 0) { // Outcome controls what the text box does, 0 just loads another textbox or object, unless there is no textbox set
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 1) { // Deactivate parent, gets rid of certain objects like encounters or battles. Use at end of dialogue trees
					this.transform.parent.gameObject.SetActive(false);
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 2) { // Adds Destiny Points to global total
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
						DestinyPoints.destinyPoints += destinyToGive;
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 3) { // Adds monster to party
					this.transform.parent.gameObject.SetActive(false);
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					for (int i = 0; i < Party.party.Length; i++) {
						if (!Party.party [i]) {
							GameObject inst = Instantiate (monsterToAdd.gameObject);
							inst.transform.position = new Vector3 (0, -10, 0);
							Party.party [i] = inst.GetComponent<Monster>();
							break;
						}
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 4) { // Adds Destiny Points to global total and deactivates parent, used with Run Away
					this.transform.parent.gameObject.SetActive(false);
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
						DestinyPoints.destinyPoints += destinyToGive;
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 5) { // Checks Charisma and branches path based on stat
					if (Party.party [0].statsMax[6] >= charismaCheck) {
						if (nextEvent != null) {
							nextEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					} else {
						if (failEvent != null) {
							failEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					}
				}
				if (outcome == 6) { // Gives item
					for (int i = 0; i < rewardItemAmount; i++) {
						if (rewardItem.shopType == 0) {
							Inventory.anInventory.addItem (rewardItem);
						} else if (rewardItem.shopType == 1) {
							Inventory.anInventory.addWeapon (rewardItem);
						} else if (rewardItem.shopType == 2) {
							Inventory.anInventory.addRelic (rewardItem);
						}
					}
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 7) { // Loads a new location, cleaning up old
					nextEvent.SetActive (true);
					failEvent.SetActive (true);
					transform.parent.gameObject.SetActive (false);
					this.gameObject.SetActive (false);
				}
				if (outcome == 8) { // Turn on or off objects
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					for (int i = 0; i < objects.Length; i++) {
						if (objects [i]) {
							if (objects [i].activeSelf) {
								objects [i].SetActive (false);
							} else {
								objects [i].SetActive (true);
							}
						}
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 9) {
					for (int i = 0; i < Party.party.Length; i++) {
						if (Party.party [i]) {
							if (Party.party [i].monsterName == monToRemove) {
								Party.party [i] = null;
								break;
							}
						}
					}
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 10) { // Adds monster to party without deactivating parent
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					for (int i = 0; i < Party.party.Length; i++) {
						if (!Party.party [i]) {
							GameObject inst = Instantiate (monsterToAdd.gameObject);
							inst.transform.position = new Vector3 (0, -10, 0);
							Party.party [i] = inst.GetComponent<Monster>();
							break;
						}
					}
					this.gameObject.SetActive (false);
				}
				if (outcome == 11) { // Victoria spell
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					Party.party [0].StatScramble ();
					this.gameObject.SetActive (false);
				}
				if (outcome == 12) { // Give Essence
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					Party.essence += (rewardItemAmount * Party.party[0].statsMax[7]);

					this.gameObject.SetActive (false);
				}
				if (outcome == 13) { // Checks Luck and branches path based on stat and random charismaCheck = base percent chance higher charismacheck = lower chance
					int rng = Random.Range(0,101);
					if ((Party.party [0].statsMax[7] + charismaCheck + rng) < 100) {
						if (nextEvent != null) {
							nextEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					} else {
						if (failEvent != null) {
							failEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					}
				}
				if (outcome == 14) { // Checks alignment and branches path. 
					if (Party.alignment > charismaCheck) {
						if (nextEvent != null) {
							nextEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					} else {
						if (failEvent != null) {
							failEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					}
				}
				if (outcome == 15) { // Checks if a certain type of monster is in the party. 
					bool yes = false;
					for (int i = 0; i < Party.party.Length; i++) {
						if (Party.party [i]) {
							if (Party.party [i].type == charismaCheck) {
								if (failEvent != null) {
									failEvent.gameObject.SetActive (true);
								}
								this.gameObject.SetActive (false);
								yes = true;
								break;
							}
						}
					}
					if (!yes) {
						if (nextEvent != null) {
							nextEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					}
				}
				if (outcome == 16) { // sets flag in flags
					if (nextEvent != null) {
						nextEvent.gameObject.SetActive (true);
					}
					Flags.flags [charismaCheck] = true;
					this.gameObject.SetActive (false);
				}
				if (outcome == 17) { // Checks flag and branches path
					if (Flags.flags[charismaCheck]) {
						if (nextEvent != null) {
							nextEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					} else {
						if (failEvent != null) {
							failEvent.gameObject.SetActive (true);
						}
						this.gameObject.SetActive (false);
					}
				}
			} 
		} else {
			if (Input.GetMouseButtonDown (0)) {
				hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				if (hit) {
					if (hit.collider.name == "Option1") {
						if (checksEssence) {
							if (Party.essence >= rewardItemAmount) {
								choiceOutcomes [0].gameObject.SetActive (true);
								this.gameObject.SetActive (false);
								Party.alignment += alignmentShift [0];
							} else {
								failEvent.gameObject.SetActive (true);
								this.gameObject.SetActive (false);
								Party.alignment += alignmentShift [0];
							}
						} else {
							choiceOutcomes [0].gameObject.SetActive (true);
							this.gameObject.SetActive (false);
							Party.alignment += alignmentShift [0];
						}
					}
					if (hit.collider.name == "Option2") {
						choiceOutcomes [1].gameObject.SetActive (true);
						this.gameObject.SetActive (false);
						Party.alignment += alignmentShift [1];
					}
					if (hit.collider.name == "Option3") {
						choiceOutcomes [2].gameObject.SetActive (true);
						this.gameObject.SetActive (false);
						Party.alignment += alignmentShift [2];
					}
				}
			} 
		}
	}

}
