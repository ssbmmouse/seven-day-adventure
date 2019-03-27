using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellConfirm : MonoBehaviour {
	public TextMesh itemName;
	public TextMesh itemDescription;
	public TextMesh howMany;
	public TextMesh price;
	public TextMesh essence;
	public TextMesh after;
	public string[] sought;
	RaycastHit2D hit;
	public static Item theItem;
	public static int mode;
	int quantity;
	int total;
	int left;

	public GameObject sellWindow;
	// Use this for initialization
	void OnEnable () {
		quantity = 1;
		essence.text = Party.essence.ToString();
		total = theItem.value/2;
		left = Party.essence + total;
		itemName.text = theItem.itemName;
		itemDescription.text = theItem.itemInfo;
	}

	// Update is called once per frame
	void Update () {
		total = quantity * (theItem.value/2);
		left = Party.essence + total;
		price.text = total.ToString ();
		after.text = left.ToString ();
		howMany.text = quantity.ToString ();
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Cancel") {
					sellWindow.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "+") {
					if (quantity < theItem.quantity) {
						quantity += 1;
					}
				}
				if (hit.collider.name == "-") {
					quantity -= 1;
					if (quantity < 0) {
						quantity = 0;
					}
				}
				if (hit.collider.name == "Sell") {
					Party.essence += total;
					if (mode == 0) {
						for (int i = 0; i < quantity; i++) {
							Inventory.anInventory.removeItem (theItem);
						}
					}
					if (mode == 1) {
						for (int i = 0; i < quantity; i++) {
							Inventory.anInventory.removeWeapon (theItem);
						}
					}
					if (mode == 2) {
						for (int i = 0; i < quantity; i++) {
							Inventory.anInventory.removeRelic (theItem);
						}
					}
					sellWindow.SetActive (true);
					this.gameObject.SetActive (false);

				}
			}
		}
	}
}
