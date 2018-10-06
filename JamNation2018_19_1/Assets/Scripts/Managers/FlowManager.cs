using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowManager {

    #region Singleton
    private static FlowManager instance;

    private FlowManager() { }

    public static FlowManager Instance {
        get {
            if (instance == null)
                instance = new FlowManager();

            return instance;
        }
    }
    #endregion

    public GV.SCENENAMES currentScene;
    Flow currentFlow;
    bool flowInitialized = false;

    public void InitializeFlowManager(GV.SCENENAMES _scene) {
        TimerManager.Instance.Init();
        TheGameManager.Instance.Init();


        currentScene = _scene;
        currentFlow = CreateFlow(_scene);
    }

    public void Update(float _dt) {
        if (currentFlow != null && flowInitialized)
            currentFlow.UpdateFlow(_dt);

        TimerManager.Instance.Update(_dt);
        TheGameManager.Instance.Update();
    }

    public void FixedUpdate(float _dt) {
        if (currentFlow != null && flowInitialized)
            currentFlow.FixedUpdateFlow(_dt);
    }

    public void ChangeFlows(GV.SCENENAMES _flowToLoad) {
        flowInitialized = false;
        currentFlow.Finish();
        currentFlow = CreateFlow(_flowToLoad);
    }

    private Flow CreateFlow(GV.SCENENAMES _flow) {
        Flow flow;

        switch (_flow) {
            case GV.SCENENAMES.MainScene:
                flow = new MainFlow();
                break;
            case GV.SCENENAMES.Puzzle1:
                flow = new Puzzle1();
                break;
            case GV.SCENENAMES.Puzzle2:
                flow = new Puzzle2();
                break;
            case GV.SCENENAMES.Puzzle3:
                flow = new Puzzle3();
                break;
            case GV.SCENENAMES.Puzzle4:
                flow = new Puzzle4();
                break;
            case GV.SCENENAMES.Puzzle5:
                flow = new Puzzle5();
                break;
            case GV.SCENENAMES.Puzzle6:
                flow = new Puzzle6();
                break;
            case GV.SCENENAMES.Puzzle7:
                flow = new Puzzle7();
                break;
            case GV.SCENENAMES.Puzzle8:
                flow = new Puzzle8();
                break;
            case GV.SCENENAMES.Puzzle9:
                flow = new Puzzle9();
                break;
            default:
                flow = null;
                break;
        }

        if(flow != null)
            SceneManager.Instance.LoadScene(_flow.ToString(), SceneLoaded);

        return flow;
    }

    public void SceneLoaded() {
        currentFlow.InitializeFlow();
        flowInitialized = true;
    }
}
