using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Init() {
        Canvas = GameObject.Find("Canvas");

        // TO DELETE AFTER INPUT MANAGED
        Start();
    }

    public void Update() {
        //TODO Manage INPUT


    }

    
    private void Start() {
        Canvas.SetActive(false);
        TheGameManager.Instance.LaunchGame();
    }
    private void Patch() {}
    private void Quit() {
        Application.Quit();
    }


}
