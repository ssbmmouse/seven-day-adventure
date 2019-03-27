using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour { 	// This huge script controls the general flow of a battle. Recommended to copy and paste an already existing
										// Battle so it's already set up (just change monsters)
	public static Monster[] activeMonsters = new Monster[4]; 
	public Monster[] monsters; 		// Set in editor, these will be the monsters in the battle
	public GameObject runEvent; 	// Set in editor, a gameObject which will be enabled if the player runs
	public GameObject loseEvent; 	// Set in editor, a gameObject which will be enabled if the player loses in specific fights
	public static bool battling; 	// If a battle is happening, this will be true. Used by various scripts to know whether a battle is happening
	public ActionGauge[] actionGauges; 	// Set in editor, should be one for each character in the HUD
	public AbilitySwitcher switcher;	// Set in editor, should be in HUD
	public TextMesh[] activeAbilities; 	// Set in editor, one for each character in hud, shows which ability each party member is using
	bool[] deads = new bool[8];			// Used to keep track of who's dead or not in a battle. If all of party or monsters are dead, the outcome is determined based on which
	public bool restrictRunning;		// Used to determine whether running should be allowed

	public GameObject itemScreen;	// Set in editor, when enabled allows the player to select an item to use
	public GameObject battleHUD;	// Set in editor, when enabled shows the HUD
	public static int itemCooldown;	// This is the value which counts up to determine if an item can be used in battle
	int itemTimeMax = 900;			// if the itemCooldown is greater than this, an item may be used
	public static bool canItem;		// This is the bool which is adjusted when itemCooldown > itemTimeMax
	public static int runCooldown;	// This is similar to itemCooldown, but for running away
	int runTimeMax = 600;			//
	public static bool canRun;		//
	public static bool ranAway;		// Set this to true if the player ran away successfully
	RaycastHit2D hit;				

	public GameObject victoryMessage; 	// The gameObject to load if the player wins
	public GameObject runAway;		  	// The gameObject to load if the player runs
	public GameObject cantRun;			// A message displayed if the player isn't able to run and tries to
	int i;
	
	void Start () { // Initialization
		battling = true;
		deads [4] = true; // set deads in case a certain monster slot is unused
		deads [5] = true;
		deads [6] = true;
		deads [7] = true;
		for (i = 0; i < monsters.Length; i++) { // set the activeMonsters based on whatever was assigned in the editor for Monsters
			activeMonsters [i] = monsters [i]; 
			monsters [i].actionTimer = i * 50; // set the monsters initial actionTimer, using i means the monsters' attacks will be staggered (without this if they attack all around the same time, it can be hard to understand whats going on)
			deads [i + 4] = false; // set the deads corresponding to this monster to false, since it shouldn't start dead
		}
		Monster.playerTargeting = activeMonsters [0]; // The default target is the first monster
		Target.enableTarget = true;					  // Enable the target object.
		for (i = 0; i < Party.party.Length; i++) { // Initialize the player party...
			if (Party.party [i]) {
				Party.party [i].Reset (); // Call reset (see Monster)
				actionGauges [i].gameObject.SetActive (true); // enable a actionguage for each party
				actionGauges [i].theMonster = Party.party [i]; // give the actionguage the monster it corresponds to
				deads [i] = false;	// don't start dead
			} else {
				actionGauges [i].gameObject.SetActive (false);
				deads [i] = true; // if a party slot is unoccupied, set the dead slot to true.
			}
		}
	}

	void OnEnable() { // More initialization
		ranAway = false;
		canItem = false;
		canRun = false;
		Item.inBattle = true; // Lets Item know a battle is going on, so items which can only be used in or out of battle work properly
		itemCooldown = Random.Range (20, 700); // Set a random value for item cooldown to start at, so it isn't the same each battle
		runCooldown = Random.Range (0, 580); // Set a random value for run cooldown, for the same reason as items
		if (restrictRunning) { // This is only necessary to make the actionindicator fully grown
			runCooldown = 1000;
		}
	}
	void OnDisable() { // When the battle is over..
		Item.inBattle = false;
	}
	void FixedUpdate() {
		if (battling) {
			itemCooldown += 1; // Increment every frame
			if (itemCooldown > itemTimeMax) {
				canItem = true;
			} else {
				canItem = false;
			}

			runCooldown += 1; // Increment every frame
			if (runCooldown > runTimeMax) {
				canRun = true;
			} else {
				canRun = false;
			}
		}
	}

	void Update () {
		if (ranAway && battling) { // This if statement runs when the player chooses to run away
			Target.enableTarget = false;
			battling = false;
			runEvent.SetActive (true);
		}
		for (i = 0; i < Party.party.Length; i++) { // constantly check if party members have died.. (hp < 1)
			if (Party.party [i]) {
				if (Party.party [i].dead) {
					deads [i] = true;
				}
			}
		}
		for (i = 0; i < activeMonsters.Length; i++) { // constantly check if monsters have died
			if (activeMonsters [i]) {
				if (activeMonsters [i].dead) {
					deads [i + 4] = true;
				}
			}
		}
		if (deads [0] && deads [1] && deads [2] && deads [3] && battling) { // This is the condition for the player losing the battle
			StartCoroutine (Defeat ());
			Target.enableTarget = false;
		}
		if (deads [4] && deads [5] && deads [6] && deads [7] && battling) { // This is the condition for the player winning the battle
			victoryMessage.SetActive (true);
			Target.enableTarget = false;
			battling = false;
		}
		if (Input.GetMouseButton (1)) { // If the mouse is rightclicked, get the collider its on, and change values based on the collider's name
										// used to determine when the player has rightclicked on a party member to open the ability switcher to change abilities (see AbilitySwitcher.cs)
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit && battling) {
				if (hit.collider.name == "Party Member 1") {
					switcher.theMonster = Party.party [0];
					switcher.gameObject.SetActive (true);
				}
				if (hit.collider.name == "Party Member 2") {
					switcher.theMonster = Party.party [1];
					switcher.gameObject.SetActive (true);
				}
				if (hit.collider.name == "Party Member 3") {
					switcher.theMonster = Party.party [2];
					switcher.gameObject.SetActive (true);
				}
				if (hit.collider.name == "Party Member 4") {
					switcher.theMonster = Party.party [3];
					switcher.gameObject.SetActive (true);
				}

			}
		}
		if (Input.GetMouseButton (0)) { // Same as above but with a left click, will activate the ITEM and RUN AWAY buttons if clicked on
			hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hit && battling) {
				if (hit.collider.name == "ITEM" && canItem) {
					battleHUD.gameObject.SetActive (false);
					itemScreen.SetActive (true);
					battling = false;
				}
				if (hit.collider.name == "RUN AWAY" && canRun) {
					if (!restrictRunning) {
						battling = false;
						runAway.SetActive (true);
					} else {
						cantRun.SetActive (true);
					}
				}
			}
		}
	

		for (i = 0; i < activeAbilities.Length; i++) { // Shows the player which ability is being used by each party member currently
			if (Party.party [i]) {
				activeAbilities [i].text = Monster.abilityNames[Party.party[i].abilityNum];
			}
		}
	}


	IEnumerator Defeat() { 	// If the player loses, this coroutine is run and what happens is the lose event is enabled, which can lead to a game over screen or
							// but it doesn't have to (so if you want a scripted death, this could just lead to what happens after)
		yield return new WaitForSeconds (2f);
		loseEvent.SetActive (true);
		this.gameObject.SetActive (false);
	}

	public bool CheckRunRestrict() { // a function which returns whether or not the player is allowed to run in this battle
		return restrictRunning;
	}
}
