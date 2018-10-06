using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public Vector3 WhereToGo;
    public float TimeToGo = 1f;

    private float CurrentLerpTime = 0;

    private Vector3 origin;
    private Vector3 CurrentPos;

    

    void Awake() {
        origin = transform.localPosition;
        CurrentPos = origin;
        WhereToGo += origin;
    }

    void Update() {
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

    private void Switch() {
        Vector3 oldOrigin = origin;
        origin = WhereToGo;
        WhereToGo = oldOrigin;

        CurrentLerpTime = 0f;
    }
}
