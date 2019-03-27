using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScreen : MonoBehaviour {
	public TextMesh[] itemNames;
	public TextMesh[] itemQuantities;
	public GameObject overworld;
	public GameObject itemConfirm;
	public GameObject partySelector;
	RaycastHit2D hit;


	public bool battleScreen;
	public GameObject battleHUD;
	// Use this for initialization
	void OnEnable () {
		for (int i = 0; i < Inventory.inventory.Length; i++) {
			if (Inventory.inventory [i]) {
				itemNames [i].gameObject.SetActive (true);
				itemNames [i].text = Inventory.inventory [i].itemName;
				itemQuantities [i].text = Inventory.inventory [i].quantity.ToString ();
			} else {
				itemNames [i].gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (!battleScreen) {
					if (hit.collider.name == "Cancel") {
						overworld.SetActive (true);
						this.gameObject.SetActive (false);
					}
				} else {
					if (hit.collider.name == "Cancel") {
						battleHUD.gameObject.SetActive (true);
						Battle.battling = true;
						this.gameObject.SetActive (false);
					}
				}
				for (int i = 0; i < itemNames.Length; i++) {
					if (hit.collider.name == "Item (" + i.ToString () + ")") {
						if (!Inventory.inventory [i].selectsMember) {
							if (!battleScreen) {
								Item.returnEvent = overworld;
								ItemConfirm.inBattle = false;
							} else {
								Item.returnEvent = battleHUD;
								ItemConfirm.inBattle = true;
							}
							ItemConfirm.item = Inventory.inventory [i];
							itemConfirm.SetActive (true);
							this.gameObject.SetActive (false);
						} else {
							if (!battleScreen) {
								Item.returnEvent = overworld;
								PartySelector.inBattle = false;
							} else {
								Item.returnEvent = battleHUD;
								PartySelector.inBattle = true;
							}
							PartySelector.item = Inventory.inventory [i];
							partySelector.SetActive (true);
							this.gameObject.SetActive (false);
						}

					}
				}
			}
		}
	}
}
