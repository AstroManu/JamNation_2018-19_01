using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManipulator : MonoBehaviour {

    private bool MoveLeftT = false;
    private bool MoveRightT = false;

    public float CamMaxSpeed = 5f;
    private float Speed = 0f;
    public float SpeedAcceleration = 2f;
    public float SpeedDeceleration = 5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float dt = Time.fixedDeltaTime;
        Vector3 origin = transform.position;

        if (MoveRightT ^ MoveLeftT) {
            if (MoveLeftT && transform.position.x > 0) {
                Speed -= dt * SpeedAcceleration;
            }
            if (MoveRightT) {
                Speed += dt * SpeedAcceleration;
            }
        }
        else {
            if(Speed > 0)
                Speed -= dt * SpeedDeceleration;
            if(Speed < 0 )
                Speed += dt * SpeedDeceleration;
            if (Speed.Near(0,0.05f)) Speed = 0;
        }
        origin.x += Speed * dt;

        transform.position = origin;

        MoveLeftT = false;
        MoveRightT = false;
    }

    public void MoveLeft() {
        MoveLeftT = true;
    }
    public void MoveRight() {
        MoveRightT = true;
    }

}
