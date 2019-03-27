using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySelector : MonoBehaviour {
	public static Item item;
	public TextMesh[] info;
	public TextMesh itemInfo;
	RaycastHit2D hit;
	public TextMesh itemDisplay;
	public GameObject itemScreen;
	public static bool inBattle;
	// Use this for initialization
	void OnEnable () {
		itemDisplay.text = item.itemName;
		itemInfo.text = item.itemInfo;
		for (int i = 0; i < Party.party.Length; i++) {
			if (Party.party [i]) {
				info [i].text = Party.party [i].monsterName;
				info [i].gameObject.SetActive (true);
			} else {
				info [i].gameObject.SetActive (false);
			}
		} 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit) {
				if (hit.collider.name == "Cancel") {
					itemScreen.SetActive (true);
					this.gameObject.SetActive (false);
				}
				for (int i = 0; i < info.Length; i++) {
					if (hit.collider.name == "Party" + i.ToString ()) {
						item.target = Party.party [i-1];
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
				}
			}
		}
	}
}
