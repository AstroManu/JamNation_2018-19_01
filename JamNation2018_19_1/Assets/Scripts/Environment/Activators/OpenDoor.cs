using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : ActivatorOUT {
    private GameObject pivot;
    public bool openLeft = true;


    public float Speed = 1f;
    private bool Activ = false;
    private float passedTime;

    public override void Activate() {
        //Debug.Log("activated");
        Activ = true;
    }

    void Awake() {
        pivot = gameObject.transform.GetChild(0).gameObject;
    }

    void Update() {
        

        if (Activ) {
            passedTime += Time.deltaTime;
            pivot.transform.localEulerAngles = new Vector3(0, Mathf.Lerp(0, (openLeft ? 90f : -90f), passedTime * Speed), 0);

            if (pivot.transform.localEulerAngles.y < -89 || pivot.transform.localEulerAngles.y > 89) {
                Activ = false;
                //Debug.Log("Door opened");
            }
        }

    }

}
