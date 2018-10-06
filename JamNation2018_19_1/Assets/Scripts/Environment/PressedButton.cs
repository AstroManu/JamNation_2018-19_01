using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedButton : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void onCollisionEnter (Collider col) {

		GetComponent <ActivatorIN> ().Activate ();
	
	}
}
