using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class MenuManager {

    #region Singleton
    private static MenuManager instance;

    private MenuManager() { }

    public static MenuManager Instance {
        get {
            if (instance == null)
                instance = new MenuManager();

            return instance;
        }
    }
    #endregion

    private GameObject Canvas;
    private bool GoInvisible = false;
    private bool GoVisible = false;
    private Player Player1;
    private Player Player2;
    private bool GoFlash = false;
    private bool GoUnFlash = false;
    private Image flasher;

    public GameObject Player;

    private bool GameLaunched = false;

    public void Init() {
        Canvas = GameObject.Find("Canvas");

        Player1 = ReInput.players.GetPlayer("Player1");
        Player2 = ReInput.players.GetPlayer("Player2");

        Player = GameObject.Find("DefaultPlayer");
        GoFlash = false;
        flasher = GameObject.Find("Flash").GetComponent<Image>();
        flasher.color = new Color(1, 1, 1, 0);
    }

    public void ReInit() {
        Canvas = GameObject.Find("Canvas");

        Player1 = ReInput.players.GetPlayer("Player1");
        Player2 = ReInput.players.GetPlayer("Player2");

        Player = GameObject.Find("DefaultPlayer");
        GoFlash = false;
        flasher = GameObject.Find("Flash").GetComponent<Image>();
        LaunchGame();
    }

    public void Update() {
        float dt = Time.deltaTime;

        if (GoFlash) {
            Color col = flasher.color;
            col.a += dt * 6;
            flasher.color = col;

            if (col.a.Near(1)) {
                GoFlash = false;
            }
        }

        if (GoUnFlash) {
            Color col = flasher.color;
            col.a -= dt ;
            flasher.color = col;

            if (col.a.Near(0,.05f)) {
                GoUnFlash = false;
            }
        }

        if (GoInvisible) {

            foreach (Image img in Canvas.GetComponentsInChildren<Image>()) {
                if (img.transform.name != "Flash") {
                    Color col = img.color;
                    col.a -= dt;
                    img.color = col;
                }
            }

            if (Canvas.GetComponentInChildren<Image>().color.a.Near(0)) {
                GoInvisible = false;
                Canvas.SetActive(false);
            }
        }

        if (GoVisible) {
            Canvas.SetActive(true);

            foreach (Image img in Canvas.GetComponentsInChildren<Image>()) {
                if (img.transform.name != "Flash") {
                    Color col = img.color;
                    col.a += dt;
                    img.color = col;
                }
            }
            if (Canvas.GetComponentInChildren<Image>().color.a >= 0.5) {
                GoVisible = false;
            }
        }

        if (Player1.GetButtonDown("Start") || Player2.GetButtonDown("Start")) {
            if (!GameLaunched) LaunchGame();
            else Pause();
        }


        if (GameLaunched) {
            Player.transform.position = new Vector3(Player.transform.position.x + (dt * 1.2f), Player.transform.position.y, Player.transform.position.z);
        }
    }

    public void BeginTrans() {
        GameLaunched = false;
        Canvas.SetActive(true);
        GoFlash = true;

    }

    public void OutOfTrans() {
        Canvas.SetActive(true);
        flasher.color = new Color(1, 1, 1, 1);

        GoUnFlash = true;
    }

    private void Pause() {
        TheGameManager.Instance.PauseGame();
        GoVisible = true;
        GameLaunched = false;
    }

    private void LaunchGame() {
        TheGameManager.Instance.LaunchGame();
        GoInvisible = true;
        GameLaunched = true;
    }
    
    private void Start() {
        TheGameManager.Instance.LaunchGame();
    }

    private void Quit() {
        Application.Quit();
    }


}
