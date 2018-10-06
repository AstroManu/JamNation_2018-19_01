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

    private GameObject Selector;
    private int Selection = 0;

    public void Init() {
        Selector = GameObject.Find("selector");

        // TO DELETE AFTER INPUT MANAGED
        Start();
    }

    public void Update() {
        //TODO Manage INPUT


    }

    private void PressConfirm() {
        switch (Selection) {
            case 0: Start();
                break;
            case 1: Quit();
                break;
            default:
                break;
        }
    }

    private void PressUp() {
        Selection = 0;
        Selector.transform.localPosition = new Vector3(Selector.transform.localPosition.x, 5, Selector.transform.localPosition.z);
    }

    private void PressDown() {
        Selection = 1;
        Selector.transform.localPosition = new Vector3(Selector.transform.localPosition.x, -30, Selector.transform.localPosition.z);
    }

    private void Start() {
        SceneManager.Instance.LoadScene(GV.SCENENAMES.MainScene.ToString(), Patch);
    }
    private void Patch() {}
    private void Quit() {
        Application.Quit();
    }


}
