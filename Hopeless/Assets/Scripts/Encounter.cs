using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour {
	public int dayToOccur;
	public int timeToOccur; // 0 = Morning, 1 = Day, 2 = Night
	public bool encountered;
	public bool dayLimited;
	public bool repeatable;
	public bool notTimeLimited;
	public bool mustBeFlagged;
	public int flag;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
