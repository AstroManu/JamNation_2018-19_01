using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class SceneManager {

    #region singleton
    private static SceneManager instance;

    private SceneManager() { }

    public static SceneManager Instance {
        get {
            if (instance == null)
                instance = new SceneManager();

            return instance;
        }
    }
    #endregion singleton

    public string CurrentScene { get; private set; }

    private UnityAction<Scene, LoadSceneMode> lastHandler = null;
    public delegate void InitToCall();
    private InitToCall onSceneLoadedDelegate;

    private void LoadScene(string sceneName, UnityAction<Scene, LoadSceneMode> handler = null) {
        if (lastHandler != null) UnityEngine.SceneManagement.SceneManager.sceneLoaded -= lastHandler;
        CurrentScene = sceneName;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        if (handler != null) {
            lastHandler = handler;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += handler;
        }
    }

    public void LoadScene(string sceneName, InitToCall handler = null) {
        onSceneLoadedDelegate = handler;
        LoadScene(sceneName, DelegateCaller);
    }

    private void DelegateCaller(Scene scene, LoadSceneMode load) {
        onSceneLoadedDelegate.Invoke();
    }



}
