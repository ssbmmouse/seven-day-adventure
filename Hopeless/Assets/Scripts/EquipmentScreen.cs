using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentScreen : MonoBehaviour {
	public static int partyNum;
	public static int mode;
	public TextMesh modeIndicator;

	public TextMesh[] itemNames;
	public TextMesh[] itemQuantities;
	public GameObject partyScreen;
	RaycastHit2D hit;
	Item item;
	void OnEnable () {
		if (mode == 0) {//weapons
			modeIndicator.text = "Weapons";
			for (int i = 0; i < Inventory.weapons.Length; i++) {
				if (Inventory.weapons [i]) {
					itemNames [i].gameObject.SetActive (true);
					itemNames [i].text = Inventory.weapons [i].itemName;
					itemQuantities [i].text = Inventory.weapons [i].quantity.ToString ();
				} else {
					itemNames [i].gameObject.SetActive (false);
				}
			}
		} if (mode == 1 || mode == 2) {
			modeIndicator.text = "Relics";
			for (int i = 0; i < Inventory.relics.Length; i++) {
				if (Inventory.relics [i]) {
					itemNames [i].gameObject.SetActive (true);
					itemNames [i].text = Inventory.relics [i].itemName;
					itemQuantities [i].text = Inventory.relics [i].quantity.ToString ();
				} else {
					itemNames [i].gameObject.SetActive (false);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				for (int i = 0; i < itemNames.Length; i++) {
					if (hit.collider.name == "Item (" + i.ToString() + ")") {
						if (mode == 0) {
							if (Party.party [partyNum].equips [mode] != Inventory.nothing) {
								Inventory.anInventory.addWeapon (Party.party [partyNum].equips [mode]);
							}
							Party.party [partyNum].Equip (Inventory.weapons [i], mode);
							Inventory.anInventory.removeWeapon (Inventory.weapons [i]);
						}
						if (mode == 1 || mode == 2) {
							if (Party.party [partyNum].equips [mode] != Inventory.nothing) {
								Inventory.anInventory.addRelic (Party.party [partyNum].equips [mode]);
							}
							Party.party [partyNum].Equip (Inventory.relics [i], mode);
							Inventory.anInventory.removeRelic (Inventory.relics [i]);
						}
						partyScreen.SetActive (true);
						this.gameObject.SetActive (false);
					}
					if (hit.collider.name == "Cancel") {
						partyScreen.SetActive (true);
						this.gameObject.SetActive (false);
					}
					if (hit.collider.name == "Unequip") {
						if (mode == 0) {
							if (Party.party [partyNum].equips [0] != Inventory.nothing) {
								Inventory.anInventory.addWeapon (Party.party [partyNum].equips [0]);
								Party.party [partyNum].equips [0] = Inventory.nothing;
								Party.party [partyNum].CalcStats ();
							}
						}
						if (mode == 1 || mode == 2) {
							if (Party.party [partyNum].equips [mode] != Inventory.nothing) {
								Inventory.anInventory.addRelic (Party.party [partyNum].equips [mode]);
								Party.party [partyNum].equips [mode] = Inventory.nothing;
								Party.party [partyNum].CalcStats ();
							}

						}
						partyScreen.SetActive (true);
						this.gameObject.SetActive (false);
					}
				}
			}
		}
	}
}
