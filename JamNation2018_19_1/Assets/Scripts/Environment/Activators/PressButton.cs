using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : ActivatorIN {

    public float Sensibility;
    
    private Vector3 basePos;
    private bool trigger = true;


    void Awake() {
        basePos = transform.localPosition;
        
    }

    void Update() {
        if (trigger) {
            if (basePos.y - Sensibility > transform.localPosition.y ) {
                trigger = false;
                Activate();
                //Debug.Log("Pushed");
            }
        }
    }


}
