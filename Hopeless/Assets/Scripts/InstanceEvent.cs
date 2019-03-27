using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceEvent : MonoBehaviour {
	public GameObject whatToInstance;
	public GameObject disableThis;
	GameObject inst;
	// Use this for initialization
	void OnEnable () {
		disableThis.SetActive (false);
		inst = Instantiate (whatToInstance);
		inst.transform.position = whatToInstance.transform.position;
		inst.transform.parent = whatToInstance.transform.parent;
		inst.transform.localScale = whatToInstance.transform.localScale;
		inst.gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
