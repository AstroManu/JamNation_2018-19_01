using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheGameManager {


    #region Singleton
    private static TheGameManager instance;

    private TheGameManager() { }

    public static TheGameManager Instance {
        get {
            if (instance == null)
                instance = new TheGameManager();

            return instance;
        }
    }
    #endregion

    public bool InPuzzle { get; private set;}
    public int WinnedPuzzle { get; private set; }
    public int CurrentPuzzle { get; private set; }

   

    public void Init() {
        WinnedPuzzle = 0;
        CurrentPuzzle = 0;

    }

    public void Update() {

       

    }

    public void LaunchGame() {
        TimerManager.Instance.InGame = true;
        InPuzzle = false;
    }

    public void PauseGame() {
        TimerManager.Instance.InGame = false;
    }


    public void EndCurrentPuzzle(bool win) {
        if (win) WinnedPuzzle++;
        InPuzzle = false;

        //TODO End transition

        SceneManager.Instance.LoadScene("MainScene");

    }



    public void BeginNextPuzzle() {
        CurrentPuzzle++;
        MenuManager.Instance.BeginTrans();
    }

    private void StartNextPuzzle() {
        SceneManager.Instance.LoadScene("Puzzle"+CurrentPuzzle);
    }

}
