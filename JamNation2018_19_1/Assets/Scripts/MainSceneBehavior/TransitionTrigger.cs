using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour {



    void OnTriggerEnter(Collider col) {
        Debug.Log("Triggered");
        TheGameManager.Instance.BeginNextPuzzle();
        Destroy(gameObject);
    }
}
