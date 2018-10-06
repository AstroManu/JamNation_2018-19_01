using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void Init() {
        WinnedPuzzle = 0;
    }

    public void Update() {

    }


    public void EndCurrentPuzzle(bool win) {
        if (win) WinnedPuzzle++;
        InPuzzle = false;

        //TODO Launch transition
    }



}
