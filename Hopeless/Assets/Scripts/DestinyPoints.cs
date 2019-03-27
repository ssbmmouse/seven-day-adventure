using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyPoints : MonoBehaviour {
	public Monster playerChar;
	public static int destinyPoints;
	int[] startingPoints = new int[6];
	RaycastHit2D hit;
	int i;
	public GameObject nextEvent;
	public TextMesh[] learnableAbilities;
	public TextMesh[] cost;
	public TextMesh[] playerInfo;
	bool[] setLearnedOnExit = new bool[20];
	// Use this for initialization
	void Start () {
		for (i = 0; i < startingPoints.Length; i++) {
			startingPoints [i] = playerChar.statsMax [i + 2];
		}
	}

	void OnDisable() {
		for (i = 0; i < learnableAbilities.Length; i++) {
			learnableAbilities [i].gameObject.SetActive (false);
		}
	}

	void OnEnable() {
		UpdateAbilities ();
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F5)) {
			destinyPoints += 10;
		}
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "+Might") {
					if (destinyPoints > 0) {
						playerChar.statsMax [2] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Might") {
					if (playerChar.statsMax [2] > startingPoints[0]) {
						playerChar.statsMax [2] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "+Will") {
					if (destinyPoints > 0) {
						playerChar.statsMax [3] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Will") {
					if (playerChar.statsMax [3] > startingPoints[1]) {
						playerChar.statsMax [3] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "+Magic") {
					if (destinyPoints > 0) {
						playerChar.statsMax [4] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Magic") {
					if (playerChar.statsMax [4] > startingPoints[2]) {
						playerChar.statsMax [4] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "+Speed") {
					if (destinyPoints > 0) {
						playerChar.statsMax [5] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Speed") {
					if (playerChar.statsMax [5] > startingPoints[3]) {
						playerChar.statsMax [5] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "+Charisma") {
					if (destinyPoints > 0) {
						playerChar.statsMax [6] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Charisma") {
					if (playerChar.statsMax [6] > startingPoints[4]) {
						playerChar.statsMax [6] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "+Luck") {
					if (destinyPoints > 0) {
						playerChar.statsMax [7] += 1;
						destinyPoints -= 1;
					}
				}
				if (hit.collider.name == "-Luck") {
					if (playerChar.statsMax [7] > startingPoints[5]) {
						playerChar.statsMax [7] -= 1;
						destinyPoints += 1;
					}
				}
				if (hit.collider.name == "Confirm") {
					nextEvent.SetActive (true);
					for (i = 3; i < playerChar.knownAbilities.Length; i++) {
						if (setLearnedOnExit [i]) {
							playerChar.knownAbilities [i] = true;
						}
					}
					this.gameObject.SetActive (false);
				}
				for (i = 0; i < learnableAbilities.Length; i++) {
					if (hit.collider.name == "Ability (" + i.ToString () + ")") {
						if (learnableAbilities [i].text == "Kick") { // Kick
							if (!setLearnedOnExit [3] && destinyPoints >= 2) {
								destinyPoints -= 2;
								setLearnedOnExit [3] = true;
								learnableAbilities [i].transform.GetChild (1).GetComponent<SpriteRenderer> ().color = Color.green;
							} else if (setLearnedOnExit[3]) {
								destinyPoints += 2;
								setLearnedOnExit [3] = false;
								learnableAbilities [i].transform.GetChild (1).GetComponent<SpriteRenderer> ().color = Color.red;
							}
						}
					}
				}
				UpdateAbilities ();
			}
		}
 

		playerInfo [0].text = playerChar.statsMax [2].ToString();
		playerInfo [1].text = playerChar.statsMax [3].ToString();
		playerInfo [2].text = playerChar.statsMax [4].ToString();
		playerInfo [3].text = playerChar.statsMax [5].ToString();
		playerInfo [4].text = playerChar.statsMax [6].ToString();
		playerInfo [5].text = playerChar.statsMax [7].ToString();
		playerInfo [6].text = destinyPoints.ToString();
		playerInfo [7].text = playerChar.statsMax [0].ToString();
		playerInfo [8].text = playerChar.statsMax [1].ToString();
		playerInfo [9].text = playerChar.monsterName;
	}
	void UpdateAbilities() {
		for (i = 0; i < learnableAbilities.Length; i++) {
			learnableAbilities [i].gameObject.SetActive (false);
		}
		int j = 0;
		if (playerChar.statsMax [2] > 2 && !playerChar.knownAbilities [3]) { // Kick
			learnableAbilities [j].gameObject.SetActive (true);
			learnableAbilities [j].text = "Kick";
			cost [j].text = "2 DP";
			j += 1;
		}
	}
}
