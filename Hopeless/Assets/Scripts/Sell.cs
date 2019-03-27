using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell : MonoBehaviour {
	public TextMesh[] items;
	public TextMesh[] quantities;
	public int mode;
	RaycastHit2D hit;
	public TextMesh modeText;
	public GameObject nextEvent;
	public GameObject sellConfirm;

	public TextMesh essence;
	// Use this for initialization
	void OnEnable () {
		essence.text = Party.essence.ToString ();
		mode = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (mode == 0) {
			modeText.text = "Items";
			for (int i = 0; i < items.Length; i++) {
				if (Inventory.inventory [i]) {
					items [i].text = Inventory.inventory [i].itemName;
					quantities [i].text = "x" + Inventory.inventory [i].quantity.ToString ();
					items [i].gameObject.SetActive (true);
					quantities [i].gameObject.SetActive (true);
				} else {
					items [i].gameObject.SetActive (false);
					quantities [i].gameObject.SetActive (false);
				}
			}
		}
		if (mode == 1) {
			modeText.text = "Weapons";
			for (int i = 0; i < items.Length; i++) {
				if (Inventory.weapons [i]) {
					items [i].text = Inventory.weapons [i].itemName;
					quantities [i].text = "x" + Inventory.weapons [i].quantity.ToString ();
					items [i].gameObject.SetActive (true);
					quantities [i].gameObject.SetActive (true);
				} else {
					items [i].gameObject.SetActive (false);
					quantities [i].gameObject.SetActive (false);
				}
			}
		}
		if (mode == 2) {
			modeText.text = "Relics";
			for (int i = 0; i < items.Length; i++) {
				if (Inventory.relics [i]) {
					items [i].text = Inventory.relics [i].itemName;
					quantities [i].text = "x" + Inventory.relics [i].quantity.ToString ();
					items [i].gameObject.SetActive (true);
					quantities [i].gameObject.SetActive (true);
				} else {
					items [i].gameObject.SetActive (false);
					quantities [i].gameObject.SetActive (false);
				}
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "+") {
					mode += 1;
					if (mode > 2) {
						mode = 0;
					}
				}
				if (hit.collider.name == "-") {
					mode -= 1;
					if (mode < 0) {
						mode = 2;
					}
				}
				if (hit.collider.name == "Done") {
					nextEvent.SetActive (true);
					this.gameObject.SetActive (false);
				}
				for (int i = 0; i < items.Length; i++) {
					if (hit.collider.name == "Item (" + i.ToString () + ")") {
						if (mode == 0) {
							SellConfirm.mode = 0;
							SellConfirm.theItem = Inventory.inventory [i];
							sellConfirm.SetActive (true);
							this.gameObject.SetActive (false);
						}
						if (mode == 1) {
							SellConfirm.mode = 1;
							SellConfirm.theItem = Inventory.weapons [i];
							sellConfirm.SetActive (true);
							this.gameObject.SetActive (false);
						}
						if (mode == 2) {
							SellConfirm.mode = 2;
							SellConfirm.theItem = Inventory.relics [i];
							sellConfirm.SetActive (true);
							this.gameObject.SetActive (false);
						}
					}
				}
			}
		}
	}
}
