using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyConfirm : MonoBehaviour {
	public TextMesh itemName;
	public TextMesh itemDescription;
	public TextMesh howMany;
	public TextMesh price;
	public TextMesh essence;
	public TextMesh after;

	RaycastHit2D hit;
	public static Item theItem;

	int quantity;
	int total;
	int left;

	public GameObject buyWindow;
	// Use this for initialization
	void OnEnable () {
		quantity = 1;
		essence.text = Party.essence.ToString();
		total = theItem.value;
		left = Party.essence - total;
		itemName.text = theItem.itemName;
		itemDescription.text = theItem.itemInfo;
	}
	
	// Update is called once per frame
	void Update () {
		total = quantity * theItem.value;
		left = Party.essence - total;
		price.text = total.ToString ();
		after.text = left.ToString ();
		howMany.text = quantity.ToString ();
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Cancel") {
					buyWindow.SetActive (true);
					this.gameObject.SetActive (false);
				}
				if (hit.collider.name == "+") {
					quantity += 1;
				}
				if (hit.collider.name == "-") {
					quantity -= 1;
					if (quantity < 0) {
						quantity = 0;
					}
				}
				if (hit.collider.name == "Buy") {
					if (Party.essence >= total) {
						Party.essence -= total;
						if (theItem.shopType == 0) {
							for (int i = 0; i < quantity; i++) {
								Inventory.anInventory.addItem (theItem);
							}
						}
						if (theItem.shopType == 1) {
							for (int i = 0; i < quantity; i++) {
								Inventory.anInventory.addWeapon (theItem);
							}
						}
						if (theItem.shopType == 2) {
							for (int i = 0; i < quantity; i++) {
								Inventory.anInventory.addRelic (theItem);
							}
						}
						buyWindow.SetActive (true);
						this.gameObject.SetActive (false);
					}
				}
			}
		}
	}
}
