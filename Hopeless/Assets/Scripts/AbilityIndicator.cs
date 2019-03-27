using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour { // Shows which ability an enemy monster is using over its head before using it
	public static GameObject anIndicator;
	int lifetime = 45;
	int counter = 0;
	public TextMesh abilityName; // Text selected from editor
	Color oldColor;

	void Start () {
		abilityName.color = Color.red; // Sets the text to red
		// This block of code allows for a gameObject of this type to easily be instantiated by other scripts
		// By using Instantiate(AbilityIndicator.anIndicator). It does this by making the first AbilityIndicator
		// (in the scene by default and offscreen) the "anIndicator" since the block of code doesn't run if there already
		// is an "anIndicator"
		if (!anIndicator) {
			anIndicator = this.gameObject;
		}
		//
	}
	
	void FixedUpdate () {
		counter += 1; // This counter system makes the text flash white, for dramatic effect, for 4 frames
		if (counter == 6) { 
			abilityName.color = Color.white;
		}
		if (counter == 10) {
			abilityName.color = Color.red;
		}
		if (counter > lifetime && this.gameObject != anIndicator) { // Once the counter is greater than the lifetime, the ability
																	// indicator is destroyed, as it shouldn't be staying on screen too long
			Destroy (this.gameObject);
		}
	}
		
}
