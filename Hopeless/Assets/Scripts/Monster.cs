using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
	public int[] statsMax = new int[8]; // 0 = HP, 1 = MP, 2 = Might, 3 = Willpower, 4 = Magic, 5 = Speed, 6 = Charisma, 7 = luck
	int[] statsAt = new int[8];
	int[] statMods = new int[8];
	int i;
	int j;
	int damage;
	public string monsterName;
	public int actionTimer;
	int actionMax = 300;
	public bool playerMonster;
	public bool dead;

	Monster target;
	public static Monster playerTargeting;
	Monster[] possibleTargets = new Monster[4];
	SpriteRenderer r;
	BattleAnimation anim;
	bool startFadeOut;
	public int abilityNum;
	public bool[] knownAbilities = new bool[12];
	public static string[] abilityNames = new string[12];
	public static int[] mpCosts = new int[12];

	int punches;
	public int type; // Determines race (Demon, human, angel, etc.)

	public Item[] equips = new Item[3];
	// Use this for initialization
	void Start () {
		abilityNames [0] = "Punch";
		abilityNames [1] = "Beam";
		abilityNames [2] = "Slash";
		abilityNames [3] = "Kick";
		abilityNames [5] = "Drive";
		mpCosts [0] = 0;
		mpCosts [1] = 1;
		mpCosts [2] = 0;
		mpCosts [3] = 0;
		mpCosts [4] = 0;
		if (playerMonster) {
			knownAbilities [0] = true;
		}
		r = GetComponent<SpriteRenderer> ();
		if (name == "Player") {
			Party.party [0] = this;
			playerMonster = true;
		}
			
		statsMax [0] = (10 + statsMax[2] + (statsMax[3]*2));
		statsMax [1] = (5 + (statsMax[4]*2) + statsMax[6]);
		for (i = 0; i < statsAt.Length; i++) {
			statsAt [i] = statsMax [i] + statMods[i];
		}
		startFadeOut = false;
		if (equips [0] == null) {
			equips [0] = Inventory.nothing;
		}
		if (equips [1] == null) {
			equips [1] = Inventory.nothing;
		}
		if (equips [2] == null) {
			equips [2] = Inventory.nothing;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		statsMax [0] = (10 + statsMax[2] + (statsMax[3]*2));
		statsMax [1] = (5 + (statsMax[4]*2) + statsMax[6]);
		if (Battle.battling && !dead) {
			actionTimer += 1 + (statsAt[5]/8);
		}

		if (statsAt [0] < 1) {
			dead = true;
			if (playerTargeting == this && Battle.battling) {
				for (i = 0; i < Battle.activeMonsters.Length; i++) {
					if (Battle.activeMonsters [i]) {
						if (!Battle.activeMonsters [i].dead) {
							playerTargeting = Battle.activeMonsters[i];
							break;
						}
					}
				}
			}
			if (!playerMonster) {
				startFadeOut = true;
			}
		}

		if (HUD.HUDActive) {
			SetHUD ();
		} else if (OverworldPartyInfo.active) {
			SetOverworldHUD ();
		}

		if (startFadeOut) {
			r.color = new Color (200, 0, 0, r.color.a - 0.05f);
		}
	}
		

	void Update() {
		if (statsAt [0] <= 0) {
			if (Battle.battling) {
				statsAt [0] = 0;
			} else {
				statsAt [0] = 1;
			}
		}
		if (statsAt [7] < 1) {
			statsAt [7] = 1;
		}
		if (actionTimer > actionMax && !playerMonster && !dead) {
			EnemyAttack ();
		} else if (actionTimer > actionMax && playerMonster) {
			if (this == Party.party [0]) {
				if (Input.GetButtonDown ("Party1")) {
					if (Party.party [0].statsAt [1] > mpCosts [abilityNum]) {
						FriendlyAttack ();
					}
				}
			}
			if (this == Party.party [1]) {
				if (Input.GetButtonDown ("Party2")) {
					if (Party.party [1].statsAt [1] > mpCosts [abilityNum]) {
						FriendlyAttack ();
					}
				}
			}
			if (this == Party.party [2]) {
				if (Input.GetButtonDown ("Party3")) {
					if (Party.party [2].statsAt [1] > mpCosts [abilityNum]) {
						FriendlyAttack ();
					}
				}
			} 
			if (this == Party.party [3]) {
				if (Input.GetButtonDown ("Party4")) {
					if (Party.party [3].statsAt [1] > mpCosts [abilityNum]) {
						FriendlyAttack ();
					}
				}
			} 
		}

		//Equipment Code
	}

	public void Attack(Monster target, bool timingBonus = false) {
		if (abilityNum == 0) {	// Punch
			damage = statsAt [2] - target.statsAt [3];
			if (damage <= 0) {
				damage = 1;
			}
			punches += 1;
			damage += (punches / 10);
		}
		if (abilityNum == 1) {	// Beam
			damage = statsAt [4] - ((target.statsAt [3]/2) + (target.statsAt[4]/4));
			if (damage <= 0) {
				damage = 1;
			}
		}
		if (abilityNum == 2) {	// Slash
			damage = statsAt [2] - target.statsAt [3];
			if (damage <= statsAt[2]) {
				damage = statsAt[2];
			}
		}
		if (abilityNum == 3) {	// Kick
			damage = statsAt [2] - target.statsAt [3];
			if (damage <= statsAt[2]) {
				damage = statsAt[2];
			}
		}
		if (abilityNum == 5) {	// Drive
			damage = statsAt [2] - target.statsAt[3];
			if (damage <= 0) {
				damage = 1;
			}
			damage = damage + 10;
		}

		if (timingBonus) {
			if (abilityNum != 3) {
				damage = damage * 2;
			} else {
				damage = damage * 3;
			}
		} else if (abilityNum == 3) {
			damage = 0;
		}

		target.statsAt [0] -= damage;
		ShowDamage (damage, timingBonus);
	}

	void ShowAbilityName(string theName) {
		GameObject inst = Instantiate (AbilityIndicator.anIndicator);
		inst.GetComponent<AbilityIndicator>().abilityName.text = theName;
		inst.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
	}

	void ShowDamage(int damage, bool timeHit = false) {
		GameObject inst = Instantiate (DamageNumber.aDamageNumber);
		inst.GetComponent<DamageNumber>().damage.text = "-" + damage.ToString();
		if (timeHit) {
			GameObject inst2 = Instantiate (DamageNumber.aDamageNumber);
			inst2.GetComponent<DamageNumber>().damage.text = "Great!";
			inst2.GetComponent<DamageNumber> ().otherDisplay = true;
			inst2.GetComponent<DamageNumber>().damage.color = new Color (0.8f, 0.8f, 0.2f, 1);
			inst2.transform.position = new Vector3 (playerTargeting.transform.position.x + 1f, playerTargeting.transform.position.y + 1.5f, playerTargeting.transform.position.z);
		}
		if (!playerMonster) {
			if (target == Party.party [0]) {
				inst.transform.position = new Vector3 (Party.positions [0].transform.position.x, Party.positions [0].transform.position.y + 1.5f, Party.positions [0].transform.position.z);
			}
			else if (target == Party.party [1]) {
				inst.transform.position = new Vector3 (Party.positions [1].transform.position.x, Party.positions [1].transform.position.y + 1.5f, Party.positions [1].transform.position.z);
			}
			else if (target == Party.party [2]) {
				inst.transform.position = new Vector3 (Party.positions [2].transform.position.x, Party.positions [2].transform.position.y + 1.5f, Party.positions [2].transform.position.z);
			}
			else if (target == Party.party [3]) {
				inst.transform.position = new Vector3 (Party.positions [3].transform.position.x, Party.positions [3].transform.position.y + 1.5f, Party.positions [3].transform.position.z);
			}
		} else {
			inst.transform.position = new Vector3 (playerTargeting.transform.position.x, playerTargeting.transform.position.y + 1.25f, playerTargeting.transform.position.z);
		}
	}

	void FindTarget() {
		j = 0;
		for (i = 0; i < Party.party.Length; i++) {
			if (Party.party [i]) {
				if (!Party.party [i].dead) {
					possibleTargets [j] = Party.party [i];
					j += 1;
				}
			}
		}
		target = possibleTargets [Random.Range (0, j)];	
	}

	void OnMouseDown() {
		playerTargeting = this;

	}
	public void Reset() {
		actionTimer = Random.Range(10,150);
	}

	public void Equip(Item item, int whichSlot) { // Equip code
		equips[whichSlot] = item;
		CalcStats ();
		Debug.Log (equips [0]);
	}
	public void CalcStats() { // Calculates stats adjusted with equip bonuses
		if (equips [0].itemID == 1) { // Sword
			statMods [2] += 1; // Increase Might by 1
			knownAbilities [2] = true; // Knows Slash
		} else {
			knownAbilities [2] = false;
		}
		for (i = 2; i < statsAt.Length; i++) {
			statsAt [i] = statsMax [i] + statMods[i];
		}
	}
	void SetHUD() {
		if (this == Party.party [0]) { // HUD Communication
			HUD.infoText [0].text = monsterName;
			HUD.infoText [1].text = statsAt [0].ToString();
			HUD.infoText [2].text = statsMax [0].ToString();
			HUD.infoText [3].text = statsAt [1].ToString();
			HUD.infoText [4].text = statsMax [1].ToString();
		}
		if (this == Party.party [1]) { // HUD Communication
			HUD.infoText [6].text = monsterName;
			HUD.infoText [7].text = statsAt [0].ToString();
			HUD.infoText [8].text = statsMax [0].ToString();
			HUD.infoText [9].text = statsAt [1].ToString();
			HUD.infoText [10].text = statsMax [1].ToString();
		}
		if (this == Party.party [2]) { // HUD Communication
			HUD.infoText [12].text = monsterName;
			HUD.infoText [13].text = statsAt [0].ToString();
			HUD.infoText [14].text = statsMax [0].ToString();
			HUD.infoText [15].text = statsAt [1].ToString();
			HUD.infoText [16].text = statsMax [1].ToString();
		}
		if (this == Party.party [3]) { // HUD Communication
			HUD.infoText [18].text = monsterName;
			HUD.infoText [19].text = statsAt [0].ToString();
			HUD.infoText [20].text = statsMax [0].ToString();
			HUD.infoText [21].text = statsAt [1].ToString();
			HUD.infoText [22].text = statsMax [1].ToString();
		}
	}

	void SetOverworldHUD() {
		if (this == Party.party [0]) { // HUD Communication
			OverworldPartyInfo.info [0].text = monsterName;
			OverworldPartyInfo.info [1].text = statsAt [0].ToString();
			OverworldPartyInfo.info [2].text = statsMax [0].ToString();
			OverworldPartyInfo.info [3].text = statsAt [1].ToString();
			OverworldPartyInfo.info [4].text = statsMax [1].ToString();
		}
		if (this == Party.party [1]) { // HUD Communication
			OverworldPartyInfo.info [5].text = monsterName;
			OverworldPartyInfo.info [6].text = statsAt [0].ToString();
			OverworldPartyInfo.info [7].text = statsMax [0].ToString();
			OverworldPartyInfo.info [8].text = statsAt [1].ToString();
			OverworldPartyInfo.info [9].text = statsMax [1].ToString();
		}
		if (this == Party.party [2]) { // HUD Communication
			OverworldPartyInfo.info [10].text = monsterName;
			OverworldPartyInfo.info [11].text = statsAt [0].ToString();
			OverworldPartyInfo.info [12].text = statsMax [0].ToString();
			OverworldPartyInfo.info [13].text = statsAt [1].ToString();
			OverworldPartyInfo.info [14].text = statsMax [1].ToString();
		}
		if (this == Party.party [3]) { // HUD Communication
			OverworldPartyInfo.info [15].text = monsterName;
			OverworldPartyInfo.info [16].text = statsAt [0].ToString();
			OverworldPartyInfo.info [17].text = statsMax [0].ToString();
			OverworldPartyInfo.info [18].text = statsAt [1].ToString();
			OverworldPartyInfo.info [19].text = statsMax [1].ToString();
		}
	}

	public void SetCurrentStat(int id, int setTo) {
		statsAt [id] = setTo;
	}
	public int GiveCurrentStat(int id) {
		return statsAt [id];
	}
	void FriendlyAttack() {
		actionTimer = 0;
		statsAt [1] -= mpCosts [abilityNum];
		anim = Instantiate (BattleAnimation.allAnims[abilityNum]);
		anim.gameObject.SetActive (true);
		anim.GetComponent<Animator> ().enabled = false;
		anim.monster = this;
		anim.target = playerTargeting;
	}

	void EnemyAttack() {
		actionTimer = 0;
		FindTarget ();
		SelectAIAttack ();
		anim = Instantiate (BattleAnimation.allAnims[abilityNum]);
		anim.monster = this;
		anim.target = target;
		anim.gameObject.SetActive (true);
		anim.GetComponent<Animator> ().enabled = false;
		ShowAbilityName (abilityNames[abilityNum]);
	}

	public void Heal(int amount) {
		statsAt [0] = statsAt [0] + amount;
		if (statsAt [0] > statsMax [0]) {
			statsAt [0] = statsMax [0];
		}
	}

	void SelectAIAttack() {
		int[] abilityAvail = new int[12];
		int k = 0;
		for (i = 0; i < knownAbilities.Length; i++) {
			if (knownAbilities [i]) {
				abilityAvail [k] = i;
				k += 1;
			}
		}
		int decision = Random.Range (0, k);
		abilityNum = abilityAvail [decision];
	}

	public void StatScramble() {
		statsMax [Random.Range (2, 8)] += 2;
		statsMax [Random.Range (2, 8)] -= 2;
	}
}
