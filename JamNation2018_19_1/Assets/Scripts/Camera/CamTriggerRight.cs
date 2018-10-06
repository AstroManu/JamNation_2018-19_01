using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTriggerRight : MonoBehaviour {

    CamManipulator Cam;

    // Use this for initialization
    void Start() {
        Cam = GameObject.Find("Camera").GetComponent<CamManipulator>();
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("TriggeredRight");

        if(other.GetComponent<BasePlayer>())
            Cam.MoveRight();
    }

    // Update is called once per frame
    void Update() {

    }
}
