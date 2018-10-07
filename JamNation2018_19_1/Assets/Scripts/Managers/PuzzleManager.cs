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

    private float P1endP;
    private float P2endP;
    private float RSpeed;

    private Vector3 orP1;
    private Vector3 orP2;
    private Vector3 orRot;

    private GameObject Rotor;
    private bool endTrigger = true;

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
            if (endTrigger) {

                //flash.color = new Color(0, 0, 0, 0);


                Rotor = new GameObject();
                Rotor.transform.position = Vector3.Lerp(Player1.transform.position, Player2.transform.position, .5f);

                Player1.transform.SetParent(Rotor.transform);
                Player2.transform.SetParent(Rotor.transform);

                orP1 = Player1.transform.position;
                orP2 = Player2.transform.position;
                orRot = new Vector3();

                endTrigger = false;
            }

            P1endP += dt;
            P2endP += dt;
            RSpeed += dt * 45;


            Vector3 goP1 = Vector3.Lerp(orP1, Rotor.transform.position, P1endP);
            Vector3 goP2 = Vector3.Lerp(orP2, Rotor.transform.position, P2endP);
            Vector3 Rotation =new Vector3(0, 0, RSpeed);

            Player1.transform.position = goP1;
            Player2.transform.position = goP2;

            Rotor.transform.Rotate(Rotation);

            Color col = flash.color;
            col.a += dt * .9f;
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
