using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyScreen : MonoBehaviour {
	public static int partyNum;
	public TextMesh[] info;
	RaycastHit2D hit;
	public GameObject overworld;
	public GameObject equipScreen;
	// Use this for initialization
	void OnEnable () {
		info [0].text = Party.party [partyNum].monsterName;
		info [1].text = Party.party [partyNum].statsMax[0].ToString();
		info [2].text = Party.party [partyNum].statsMax[1].ToString();
		info [3].text = Party.party [partyNum].statsMax[2].ToString();
		info [4].text = Party.party [partyNum].statsMax[3].ToString();
		info [5].text = Party.party [partyNum].statsMax[4].ToString();
		info [6].text = Party.party [partyNum].statsMax[5].ToString();
		info [7].text = Party.party [partyNum].statsMax[6].ToString();
		info [8].text = Party.party [partyNum].statsMax[7].ToString();
		info [9].text = Party.party [partyNum].equips[0].itemName;
		info [10].text = Party.party [partyNum].equips[1].itemName;
		info [11].text = Party.party [partyNum].equips[2].itemName;
		int j = 12;
		for (int i = 0; i < Party.party [partyNum].knownAbilities.Length; i++) {
			if (Party.party [partyNum].knownAbilities [i]) {
				info [j].text = Monster.abilityNames [i];
				info [j].gameObject.SetActive (true);
				j++;
			} else {
				info [j].gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Confirm") {
					overworld.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Wep") {
					EquipmentScreen.mode = 0;
					EquipmentScreen.partyNum = partyNum;
					equipScreen.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Relic") {
					EquipmentScreen.mode = 1;
					EquipmentScreen.partyNum = partyNum;
					equipScreen.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "Relic2") {
					EquipmentScreen.mode = 2;
					EquipmentScreen.partyNum = partyNum;
					equipScreen.SetActive (true);
					this.gameObject.SetActive (false);
				}
			}
		}
	}
}
