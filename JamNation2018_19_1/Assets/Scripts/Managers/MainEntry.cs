using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEntry : MonoBehaviour {

    void Awake() {
        if (GameObject.FindObjectsOfType<MainEntry>().Length > 1)
            GameObject.Destroy(gameObject);
    }

    // Use this for initialization
    void Start() {
        GameObject.DontDestroyOnLoad(gameObject);


        try {
            FlowManager.Instance.InitializeFlowManager(
                (GV.SCENENAMES)System.Enum.Parse(
                    typeof(GV.SCENENAMES),
                    UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                )
            );
        }
        catch (System.Exception e) {
            Debug.LogException(e, this);
            Debug.LogError("Your scene is not registered in GV.SCENESNAMES");
        }
    }

    // Update is called once per frame
    void Update() {
        FlowManager.Instance.Update(Time.deltaTime);
    }

    void FixedUpdate() {
        FlowManager.Instance.FixedUpdate(Time.fixedDeltaTime);
    }
}
