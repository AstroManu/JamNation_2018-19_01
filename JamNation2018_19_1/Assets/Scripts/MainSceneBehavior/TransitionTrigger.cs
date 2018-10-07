using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour {



    void OnTriggerEnter(Collider col) {

        if (col.transform.name == "DefaultPlayer") {
            TheGameManager.Instance.BeginNextPuzzle();
        }
    }
}
