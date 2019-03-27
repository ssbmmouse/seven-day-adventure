using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {
	public static bool HUDActive;
	public GameObject[] partyInfo;
	public static TextMesh[] infoText = new TextMesh[24];
	public TextMesh[] infoTextFromEditor;
	int i;
	// Use this for initialization
	void OnEnable () {
		HUDActive = true;
		for (i = 0; i < infoText.Length; i++) {
			infoText [i] = infoTextFromEditor [i];
		}

		for (i = 0; i < partyInfo.Length; i++) {
			if (Party.party [i]) {
				partyInfo [i].SetActive (true);
			} else {
				partyInfo [i].SetActive (false);
			}
		}

	}

	void OnDisable() {
		HUDActive = false;
	}
	// Update is called once per frame
	void Update () {
		for (i = 0; i < partyInfo.Length; i++) {
			if (Party.party [i]) {
				partyInfo [i].SetActive (true);
			} else {
				partyInfo [i].SetActive (false);
			}
		}
	}
}
