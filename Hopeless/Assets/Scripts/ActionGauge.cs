using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionGauge : MonoBehaviour { 	// Shows how much longer the player must wait until a monster is ready to attack, by using a bar
											// which stretches in both horizontal directions. (These can be seen in battle)
	public Monster theMonster; // Set in Editor
	Color startingColor;
	public Color readyColor; // Set in Editor

	public bool itemCooldownGauge;
	public bool runCooldown;

	public Battle battle; // Set in Editor

	void Start () {
		startingColor = GetComponent<SpriteRenderer> ().color;
	}

	void Update () {
		if (!itemCooldownGauge && !runCooldown) { 	// Once the Monster's actionTimer is over 300, an attack is ready. Therefore, set the color to 
													// the ready color if its over and don't if it's not. Have the scale of the attached gameobject
													// increase to indicate how much longer it will take until fully charged. 
			if (theMonster.actionTimer < 301) {
				GetComponent<SpriteRenderer> ().color = startingColor;
				transform.localScale = new Vector3 (theMonster.actionTimer * 0.001f, 0.06f, 0);
			} else {
				transform.localScale = new Vector3 (0.3f, 0.065f, 0);
				GetComponent<SpriteRenderer> ().color = readyColor;
			}
		} else if (itemCooldownGauge) {	// Same as above, but for the item cooldown guage (seen in battle)
			if (Battle.itemCooldown < 901) {
				GetComponent<SpriteRenderer> ().color = startingColor;
				transform.localScale = new Vector3 (Battle.itemCooldown * 0.00045f, 0.085f, 0);
			} else {
				transform.localScale = new Vector3 (0.4f, 0.09f, 0);
				GetComponent<SpriteRenderer> ().color = readyColor;
			}
		} else if (runCooldown) { // Same as above, but for the run cooldown guage (also seen in battle)
			if (battle.CheckRunRestrict () == false) { // BUT! only if the player is allowed to run
				if (Battle.runCooldown < 601) {
					GetComponent<SpriteRenderer> ().color = startingColor;
					transform.localScale = new Vector3 (Battle.runCooldown * 0.00073f, 0.085f, 0);
				} else {
					transform.localScale = new Vector3 (0.4f, 0.09f, 0);
					GetComponent<SpriteRenderer> ().color = readyColor;
				}
			} else {
				transform.localScale = new Vector3 (0.4f, 0.09f, 0); // If they can't run, it'll display as gray
				GetComponent<SpriteRenderer> ().color = Color.gray;
			}

		}
	}

}
