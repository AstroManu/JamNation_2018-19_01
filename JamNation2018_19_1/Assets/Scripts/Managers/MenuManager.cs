using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    public void Init() {
        Canvas = GameObject.Find("Canvas");
        TimerManager.Instance.CreateSimpleTimer(this, 2f, test);

        // TO DELETE AFTER INPUT MANAGED
    }

    public void Update() {
        if (GoInvisible) {
            float dt = Time.deltaTime;

            foreach (Image img in Canvas.GetComponentsInChildren<Image>()) {
                Color col = img.color;
                col.a -= dt;
                img.color = col;
            }

            if (Canvas.GetComponentInChildren<Image>().color.a <= 0) {
                GoInvisible = false;
                Canvas.SetActive(false);
            }
        }

    }

    private void test() {
        LaunchGame();
        Debug.Log("GameLaunched");
    }

    private void LaunchGame() {
        TheGameManager.Instance.LaunchGame();
        GoInvisible = true;
    }
    
    private void Start() {
        TheGameManager.Instance.LaunchGame();
    }
    private void Patch() {}
    private void Quit() {
        Application.Quit();
    }


}
