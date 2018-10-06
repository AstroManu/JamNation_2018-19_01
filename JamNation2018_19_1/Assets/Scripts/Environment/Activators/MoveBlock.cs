using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : ActivatorOUT {

    public Vector3 WhereToGo;
    public float TimeToGo = 3f;

    public bool isActivate = false;

    private float CurrentLerpTime = 0;

    private Vector3 origin;
    private Vector3 CurrentPos;

    public override void Activate() {
        isActivate = !isActivate;
    }

    // Use this for initialization
    void Start () {
        origin = transform.localPosition;
        CurrentPos = origin;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActivate) {
            CurrentPos = transform.localPosition;

            CurrentLerpTime += Time.deltaTime;
            if (CurrentLerpTime > TimeToGo) {
                CurrentLerpTime = TimeToGo;
            }

            float perc = CurrentLerpTime / TimeToGo;

            CurrentPos.x = Mathf.Lerp(origin.x, WhereToGo.x, perc);
            CurrentPos.y = Mathf.Lerp(origin.y, WhereToGo.y, perc);

            if (CurrentPos.x.Near(WhereToGo.x) && CurrentPos.y.Near(WhereToGo.y)) {
                Switch();
            }

            transform.localPosition = CurrentPos;
        }
	}

    private void Switch() {
        Vector3 oldOrigin = origin;
        origin = WhereToGo;
        WhereToGo = oldOrigin;

        CurrentLerpTime = 0f;
    }
}
