using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimation : MonoBehaviour {
	public BattleAnimation[] allAnimsFromEditor;
	public static BattleAnimation[] allAnims = new BattleAnimation[12];
	public static BattleAnimation initAnim;
	public bool initializer;
	public Monster monster;
	public Monster target;
	public int duration = 1;
	int counter;
	public int timingRangeFloor;
	public int timingRangeMax;
	bool timeHit = false;
	bool attempt = false;
	public static bool animating = false;
	public static BattleAnimation[] waitingAnims = new BattleAnimation[8];
	public static int i;
	public static BattleAnimation activeAnim;
	public bool itemAnim;
	public Item theItem;

	public bool startsAtCaster;

	void Start () {
		if (!initAnim && initializer) {
			initAnim = this;
		}
		if (this == initAnim) {
			for (int i = 0; i < allAnims.Length; i++) {
				if (allAnimsFromEditor [i]) {
					allAnims [i] = allAnimsFromEditor [i];
				}
			}
		} else {
			if (!animating) {
				PlayAnimation ();
			} else {
				waitingAnims [i] = this;
				i += 1;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (this == activeAnim) {
			counter += 1;
		}
		if (!itemAnim) {
			if ((counter > duration) && this != initAnim) {
				monster.Attack (target, timeHit);
				if (i > 0) {
					i -= 1;
					waitingAnims [i].PlayAnimation ();
					waitingAnims [i] = null;
				} else {
					animating = false;
				}
				Destroy (this.gameObject);
			}
		} else {
			if ((counter > duration) && this != initAnim) {
				theItem.ActivateAnim (theItem.itemDamage);
				if (i > 0) {
					i -= 1;
					waitingAnims [i].PlayAnimation ();
					waitingAnims [i] = null;
				} else {
					animating = false;
				}
				Destroy (this.gameObject);
			}
		}
	}
	void Update() {
		if (!itemAnim && (Input.GetMouseButtonDown (0) || (monster == Party.party[0] && Input.GetButtonDown("Party1")) || (monster == Party.party[1] && Input.GetButtonDown("Party2")) || (monster == Party.party[2] && Input.GetButtonDown("Party3")) || (monster == Party.party[3] && Input.GetButtonDown("Party4")))) {
			if (counter > timingRangeFloor && counter < timingRangeMax && !attempt && monster.playerMonster) {
				timeHit = true;
				Debug.Log ("Hit!");
				ScreenFlash.startFlash = true;
			} else if (!timeHit) {
				attempt = true;
				Debug.Log ("Failure");
			}
		}
	}
	void PlayAnimation() {
		counter = 0;
		activeAnim = this;
		animating = true;
		GetComponent<Animator> ().enabled = true;
		if (!itemAnim) {
			if (monster.playerMonster) {
				if (!startsAtCaster) {
					transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -1);
				} else {
					Tractor.target = target.gameObject;
					if (Party.party [0]) {
						if (target == Party.party [0]) {
							transform.position = new Vector3 (Party.positions [0].transform.position.x, Party.positions [0].transform.position.y, -1);
						}
					}
					if (Party.party [1]) {
						if (target == Party.party [1]) {
							transform.position = new Vector3 (Party.positions [1].transform.position.x, Party.positions [1].transform.position.y, -1);
						}
					}
					if (Party.party [2]) {
						if (target == Party.party [2]) {
							transform.position = new Vector3 (Party.positions [2].transform.position.x, Party.positions [2].transform.position.y, -1);
						}
					}
					if (Party.party [3]) {
						if (target == Party.party [3]) {
							transform.position = new Vector3 (Party.positions [3].transform.position.x, Party.positions [3].transform.position.y, -1);
						}
					}
				}
			} else {
				if (!startsAtCaster) {
					if (Party.party [0]) {
						if (target == Party.party [0]) {
							transform.position = new Vector3 (Party.positions [0].transform.position.x, Party.positions [0].transform.position.y, -1);
						}
					}
					if (Party.party [1]) {
						if (target == Party.party [1]) {
							transform.position = new Vector3 (Party.positions [1].transform.position.x, Party.positions [1].transform.position.y, -1);
						}
					}
					if (Party.party [2]) {
						if (target == Party.party [2]) {
							transform.position = new Vector3 (Party.positions [2].transform.position.x, Party.positions [2].transform.position.y, -1);
						}
					}
					if (Party.party [3]) {
						if (target == Party.party [3]) {
							transform.position = new Vector3 (Party.positions [3].transform.position.x, Party.positions [3].transform.position.y, -1);
						}
					}
				} else {
					Tractor.target = Party.positions[0];
					transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, -1);
				}
			}
		} else {
			transform.position = new Vector3 (0, 0, 0);
		}

	}

	void ActivateAnim(BattleAnimation animation) {
		animation.gameObject.SetActive (true);
	}
}
