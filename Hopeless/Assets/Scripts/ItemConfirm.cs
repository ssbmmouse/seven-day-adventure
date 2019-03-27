using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConfirm : MonoBehaviour {
	public TextMesh itemDisplay;
	public TextMesh useItem;
	public TextMesh itemInfo;
	public static Item item;
	public GameObject itemMenu;
	RaycastHit2D hit;
	public static bool inBattle;
	// Use this for initialization
	void OnEnable () {
		useItem.text = "Use " + item.itemName + "?";
		itemDisplay.text = item.itemName;
		itemInfo.text = item.itemInfo;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Yes") {
					if (item.battleOnly) {
						if (inBattle) {
							item.Use ();
							this.gameObject.SetActive (false);
						}
					} else if (item.overworldOnly) {
						if (!inBattle) {
							item.Use ();
							this.gameObject.SetActive (false);
						}
					} else if (!item.battleOnly && !item.overworldOnly) {
						item.Use ();
						this.gameObject.SetActive (false);
					}


				}
				if (hit.collider.name == "No") {
					itemMenu.SetActive (true);
					this.gameObject.SetActive (false);
				}
			}
		}
	}
}
