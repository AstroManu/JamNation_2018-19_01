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

        
        //TODO Begin puzzle Transition
        
        switch (CurrentPuzzle) {
            case 1:
                SceneManager.Instance.LoadScene("Puzzle1");
                break;
            case 2:
                SceneManager.Instance.LoadScene("Puzzle2");
                break;
            case 3:
                SceneManager.Instance.LoadScene("Puzzle3");
                break;
            case 4:
                SceneManager.Instance.LoadScene("Puzzle4");
                break;
            case 5:
                SceneManager.Instance.LoadScene("Puzzle5");
                break;
            case 6:
                SceneManager.Instance.LoadScene("Puzzle6");
                break;
            case 7:
                SceneManager.Instance.LoadScene("Puzzle7");
                break;
            case 8:
                SceneManager.Instance.LoadScene("Puzzle8");
                break;
            case 9:
                SceneManager.Instance.LoadScene("Puzzle9");
                break;
            default:
                Debug.LogError("Unnable to load next Puzzle");
                break;
        }

    }

}
