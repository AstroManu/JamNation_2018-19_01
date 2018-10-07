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

    private Timer NextPzlTimer;
    private Timer EndPzlTimer;

    private Vector3 Respawn;

    public void Init() {
        WinnedPuzzle = 0;
        CurrentPuzzle = 0;
        NextPzlTimer = new Timer(.2f, StartNextPuzzle);
        EndPzlTimer = new Timer(2f, EndPuzzle);
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

        TimerManager.Instance.AddTimer(this, EndPzlTimer, TimerManager.Timebook.Global);
    }

    private void EndPuzzle() {
        SceneManager.Instance.LoadScene("MainScene",EndPuzzleHandler);

    }

    private void EndPuzzleHandler() {
        MenuManager.Instance.ReInit();
        MenuManager.Instance.OutOfTrans();

        Respawn.x += 2f;
        Respawn.y += 2f;
        MenuManager.Instance.Player.transform.localPosition = Respawn;
    }

    public void BeginNextPuzzle() {
        Respawn = MenuManager.Instance.Player.transform.localPosition;
        CurrentPuzzle++;
        MenuManager.Instance.BeginTrans();
        TimerManager.Instance.AddTimer(this, NextPzlTimer);
    }

    private void StartNextPuzzle() {
        SceneManager.Instance.LoadScene("Puzzle"+CurrentPuzzle);
    }

    
}
