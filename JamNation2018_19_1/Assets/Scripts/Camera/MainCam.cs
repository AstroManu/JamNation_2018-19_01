using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour {

    public GameObject ToFollow;

    private float XDistP;

    void Awake() {
        XDistP  = ToFollow.transform.position.x - transform.position.x;

    }

    void Update() {

        transform.position = new Vector3(ToFollow.transform.position.x - XDistP, transform.position.y, transform.position.z);

    }

}
