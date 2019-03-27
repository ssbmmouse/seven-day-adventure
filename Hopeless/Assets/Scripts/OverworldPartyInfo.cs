using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPartyInfo : MonoBehaviour {
	public static bool active;
	public static TextMesh[] info = new TextMesh[20];
	public TextMesh[] infoFromEditor;
	public GameObject[] partyMembers;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < info.Length; i++) {
			info [i] = infoFromEditor [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < Party.party.Length; i++) {
			if (Party.party [i]) {
				partyMembers [i].SetActive (true);
			} else {
				partyMembers [i].SetActive (false);
			}
		}
	}

}
