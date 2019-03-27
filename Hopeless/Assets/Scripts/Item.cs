using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
	public int itemID;
	public int quantity;
	public string itemName;
	public bool selectsMember;
	public int value;
	public Monster target;
	public int shopType;
	public bool battleOnly;
	public bool overworldOnly;

	public string itemInfo;
	public static GameObject returnEvent;
	public static GameObject lastSanctuary;
	public static bool inBattle;

	public int itemDamage;

	BattleAnimation anim;
	GameObject[] instances = new GameObject[4];
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Use() {
		if (itemID == 0) { // Milk
			target.Heal (20);
			Debug.Log (returnEvent);
			returnEvent.SetActive (true);
			if (inBattle) {
				Battle.battling = true;
				Battle.itemCooldown = 0;
				Battle.canItem = false;
			}
		}
		if (itemID == 1) { // Gate
			returnEvent.SetActive (true);
			returnEvent.transform.parent.gameObject.SetActive (false);
			lastSanctuary.transform.parent.gameObject.SetActive (true);

		}
		if (itemID == 2) { // Bomb
			anim = Instantiate(BattleAnimation.allAnims[4]);
			anim.itemAnim = true;
			anim.theItem = this;
			anim.gameObject.SetActive (true);
			returnEvent.SetActive (true);
			Battle.battling = true;
			Battle.itemCooldown = 0;
			Battle.canItem = false;
			itemDamage = 10 + (10*Random.Range(1,4));
		}
		Inventory.anInventory.removeItem (this);
	}
		

	public void ActivateAnim(int damage) {
		for (int i = 0; i < Battle.activeMonsters.Length; i++) {
			if (Battle.activeMonsters [i]) {
				if (!Battle.activeMonsters [i].dead) {
					instances [i] = Instantiate (DamageNumber.aDamageNumber);
					instances [i].GetComponent<DamageNumber> ().damage.text = "-" + damage.ToString ();
					instances [i].transform.position = new Vector3(Battle.activeMonsters [i].transform.position.x, Battle.activeMonsters [i].transform.position.y + 1, -1);
					Battle.activeMonsters [i].SetCurrentStat (0, (Battle.activeMonsters [i].GiveCurrentStat (0) - damage));
				}
			}
		}

	}
}
