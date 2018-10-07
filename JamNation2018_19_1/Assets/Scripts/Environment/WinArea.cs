using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinArea : MonoBehaviour {

    

    void OnTriggerStay(Collider col) {

        if (col.transform.name.Contains("layer")) {
            GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().PuzzleSolved();
        }
    }
}
