using System;
using System.Collections.Generic;
using UnityEngine;

public class GV {

    #region Singleton
    private static GV instance;


    private GV() {

    }

    public static GV Instance {
        get {
            if (instance == null) {
                instance = new GV();
            }
            return instance;
        }
    }

    #endregion


    // GLOBAL VARIABLES
    public enum SCENENAMES { DUMMY, MainScene, Puzzle1, Puzzle2, Puzzle3, Puzzle4, Puzzle5, Puzzle6, Puzzle7, Puzzle8, Puzzle9}
   

}