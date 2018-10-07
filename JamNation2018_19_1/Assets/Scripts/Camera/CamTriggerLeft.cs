using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerLeft : MonoBehaviour {

    CamManipulator Cam;

	// Use this for initialization
	void Start () {
        Cam = GameObject.Find("Camera").GetComponent<CamManipulator>();
	}

    private void OnTriggerStay(Collider other) {
        if (other.GetComponent<BasePlayer>())
            Cam.MoveLeft();
    }

        // Update is called once per frame
    void Update () {
		
	}
}
