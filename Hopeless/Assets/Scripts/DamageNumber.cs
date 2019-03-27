using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour {
	public static GameObject aDamageNumber;
	int lifetime = 60;
	int counter = 0;
	public TextMesh damage;

	public bool otherDisplay;
	// Use this for initialization
	void Start () {
		if (!aDamageNumber) {
			aDamageNumber = this.gameObject;
		}
		if (!otherDisplay) {
			damage.color = new Color (150, 0, 0);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		counter += 1;
		if (counter > lifetime && this.gameObject != aDamageNumber) {
			Destroy (this.gameObject);
		}
		if (this.gameObject != aDamageNumber) {
			if (!otherDisplay) {
				transform.position = new Vector3 (transform.position.x, transform.position.y - 0.01f, transform.position.z);
				damage.color = new Color (damage.color.r, damage.color.g, damage.color.b, damage.color.a - 0.01f);
			}
		}
	}
}
