using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuzzleManager : MonoBehaviour {

    public float TimeMax = 60;
    public GameObject Player1;
    public GameObject Player2;
    public bool isShiny;

    private Timer Ender;
    private Image flash;
    private GameObject canvas;

    private bool endTransition = true;
    private bool beginTransition = false;

    private Vector3 Rotator;

	// Use this for initialization
	void Start () {
        Ender = new Timer(TimeMax, PuzzleLose);
        TimerManager.Instance.AddTimer(this, Ender, TimerManager.Timebook.InGame);
        canvas = GameObject.Find("Canvas");
        flash = canvas.GetComponentInChildren<Image>();

        flash.color = new Color(1,1,1,1);

	}


    
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        if (endTransition) {
            Color col = flash.color;
            col.a -= dt;
            flash.color = col;

            if (col.a.Near(0)) {
                col.a = 0;
                flash.color = col;
                endTransition = false;
            }
        }

        if (beginTransition) {
            Color col = flash.color;
            col.a += dt * .5f;
            flash.color = col;

            if (col.a.Near(1,.05f)) {
                col.a = 1;
                flash.color = col;
                beginTransition = false;
            }
        }
        
	}


    public void PuzzleSolved() {
        Ender.Kill();
        beginTransition = true;
        Player1.GetComponent<BasePlayer>().LaunchEndingAnimation();
        Player2.GetComponent<BasePlayer>().LaunchEndingAnimation();
        TheGameManager.Instance.EndCurrentPuzzle(true);
    }

    private void PuzzleLose() {

        Player1.GetComponent<BasePlayer>().LaunchEndingAnimation();
        Player2.GetComponent<BasePlayer>().LaunchEndingAnimation();
        beginTransition = true;
        TheGameManager.Instance.EndCurrentPuzzle(false);
    }

}
