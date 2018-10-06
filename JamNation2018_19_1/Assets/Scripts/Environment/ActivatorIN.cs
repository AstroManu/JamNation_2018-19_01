using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorIN : MonoBehaviour {

    public List<GameObject> ToActivate;

    public virtual void Activate() {
        foreach (GameObject go in ToActivate) {
            go.GetComponent<ActivatorOUT>().Activate();
        }
    }
}
