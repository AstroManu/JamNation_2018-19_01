using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    public float TimeMax;

    private Timer Ender;

	// Use this for initialization
	void Start () {
        Ender = new Timer(TimeMax, PuzzleLose);
        TimerManager.Instance.AddTimer(this, Ender, TimerManager.Timebook.InGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void PuzzleSolved() {
        Ender.Kill();
        TheGameManager.Instance.EndCurrentPuzzle(true);
    }

    private void PuzzleLose() {
        TheGameManager.Instance.EndCurrentPuzzle(false);
    }

}
