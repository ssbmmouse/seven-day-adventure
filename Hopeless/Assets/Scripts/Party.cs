using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour {
	public static Monster[] party = new Monster[4];
	public Monster[] testParty = new Monster[4];
	public bool usingTestParty;
	public static GameObject[] positions = new GameObject[4];
	public static int essence = 100;
	public GameObject[] positionsInEditor;
	int i;
	public static int alignment;

	// Use this for initialization
	void Start () {
		if (usingTestParty) {
			for (i = 0; i < testParty.Length; i++) {
				party [i] = testParty [i];
			}
		}
		for (i = 0; i < positions.Length; i++) {
			positions [i] = positionsInEditor [i];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
