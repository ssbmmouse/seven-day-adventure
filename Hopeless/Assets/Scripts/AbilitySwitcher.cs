using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySwitcher : MonoBehaviour { // Allows the player to switch the current ability used in battle 
	public Monster theMonster; // Set by other scripts
	public TextMesh[] abilities; // Set in editor
	public static string[] abilityNames; // Set by other script, used to store names of all abilities
	int j;
	int i;
	RaycastHit2D hit;

	public TextMesh monName;

	void OnEnable () {
		monName.text = theMonster.monsterName; // "theMonster" is set by other scripts before this object is enabled
		for (i = 0; i < abilities.Length; i++) { // This loop initializes all text meshes to be off by default, in case they were turned on earlier
			if (abilities [i]) { 
				abilities [i].gameObject.SetActive (false);
			}
		}
		j = 0;
		for (i = 0; i < theMonster.knownAbilities.Length; i++) { 	// This loop determines if the monster knows a certain ability, and if it does
			if (theMonster.knownAbilities [i]) {					// adds it to the list of abilities in the ability switcher
				abilities [j].text = Monster.abilityNames [i];
				abilities [j].name = Monster.abilityNames [i];
				abilities [j].gameObject.SetActive (true);
				j += 1;
			}
		}
	}

	void Update() {
		if (Input.GetMouseButton (0)) { // if the mouse is over a collider, this code gets the colliders name and sets the current ability of a monster
										// based on the name. These NEED to correspond to the correct abilityNum or it will be impossible to
										// change to the ability or change to the wrong ability. (Look in Monster script for which abilityNum is which ability)
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Punch") {
					theMonster.abilityNum = 0;
				}
				if (hit.collider.name == "Beam") {
					theMonster.abilityNum = 1;
				}
				if (hit.collider.name == "Slash") {
					theMonster.abilityNum = 2;
				}
				if (hit.collider.name == "Kick") {
					theMonster.abilityNum = 3;
				}
				this.gameObject.SetActive (false);
			} else {
				this.gameObject.SetActive (false);
			}
		}
		if (Battle.battling == false) { // turn off if not battling as this isn't used out of battle (currently)
			this.gameObject.SetActive (false);
		}
	}
}
