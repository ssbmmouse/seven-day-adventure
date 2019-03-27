using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
	public TextMesh essence;
	public TextMesh[] itemsText;
	public TextMesh[] prices;
	public Item[] possibleItems;
	Item[] items = new Item[10];
	int rng;
	public GameObject nextEvent;
	RaycastHit2D hit;
	public GameObject buyConfirm;
	// Use this for initialization
	void Awake () {
		ChooseSelection ();
	}
	void OnEnable () {
		essence.text = Party.essence.ToString();
		for (int i = 0; i < itemsText.Length; i++) {
			if (items [i]) {
				itemsText [i].text = items [i].itemName;
				prices [i].text = items [i].value.ToString ();
				itemsText [i].gameObject.SetActive (true);
			} else {
				itemsText [i].gameObject.SetActive (false);
				prices [i].gameObject.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Done") {
					nextEvent.gameObject.SetActive(true);
					this.gameObject.SetActive(false);
				}
				for (int i = 0; i < itemsText.Length; i++) {
					if (hit.collider.name == "Item (" + i.ToString () + ")") {
						BuyConfirm.theItem = items [i];
						buyConfirm.SetActive (true);
						this.gameObject.SetActive (false);
					}
				}
			}
		}
	}

	void ChooseSelection() {
		items [0] = possibleItems [0];
		int j = 1;
		for (int i = 1; i < possibleItems.Length; i++) {
			if (possibleItems [i]) {
				rng = Random.Range (0, 101);
				if (rng < 20) {
					items [j] = possibleItems [i];
					j += 1;
				}
			}
		}
	}
}
