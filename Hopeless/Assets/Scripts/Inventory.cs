using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public static Inventory anInventory;
	public static Item[] inventory = new Item[20];
	public static Item[] weapons = new Item[20];
	public static Item[] relics = new Item[20];

	public Item testWeapon;
	public Item testRelic;
	public Item testItem;
	public Item testItem2;
	public Item testItem3;

	public Item nothingFromEditor;
	public static Item nothing;
	// Use this for initialization
	void Awake () {
		if (!anInventory) {
			anInventory = this;
		}
		nothing = nothingFromEditor;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F8)) {
			addItem (testItem3);
		}
	}

	public void addItem(Item item) {
		bool next = false;
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == item) {
				inventory [i].quantity += 1;
				next = false;
				break;
			} else {
				next = true;
			}
		}
		if (next) {
			for (int i = 0; i < inventory.Length; i++) {
				if (inventory [i] == null) {
					inventory [i] = item;
					inventory [i].quantity = 1; 
					break;
				}
			}
		}
	}

	public void removeItem(Item item) {
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i] == item) {
				item.quantity -= 1;
				if (item.quantity < 1) {
					inventory [i] = null;
					break;
				}
			}
		}
	}

	public void addWeapon(Item item) {
		bool next = false;
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i] == item) {
				weapons [i].quantity += 1;
				next = false;
				break;
			} else {
				next = true;
			}
		}
		if (next) {
			for (int i = 0; i < weapons.Length; i++) {
				if (!weapons [i]) {
					weapons [i] = item;
					weapons [i].quantity = 1;
					break;
				}
			}
		}
	}

	public void removeWeapon(Item item) {
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i] == item) {
				item.quantity -= 1;
				if (item.quantity < 1) {
					weapons [i] = null;
					break;
				}
			}
		}
	}


	public void addRelic(Item item) {
		bool next = false;
		for (int i = 0; i < relics.Length; i++) {
			if (relics [i] == item) {
				relics [i].quantity += 1;
				next = false;
				break;
			} else {
				next = true;
			}
		}
		if (next) {
			for (int i = 0; i < relics.Length; i++) {
				if (!relics [i]) {
					relics [i] = item;
					relics [i].quantity = 1;
					break;
				}
			}
		}
	}

	public void removeRelic(Item item) {
		for (int i = 0; i < relics.Length; i++) {
			if (relics [i] == item) {
				item.quantity -= 1;
				if (item.quantity < 1) {
					relics [i] = null;
					break;
				}
			}
		}
	}
}
