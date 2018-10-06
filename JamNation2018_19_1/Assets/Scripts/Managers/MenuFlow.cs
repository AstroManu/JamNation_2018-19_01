using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlow : Flow {

    public override void Finish() {
    }
    public override void FixedUpdateFlow(float _fdt) {
    }
    public override void InitializeFlow() {
        MenuManager.Instance.Init();
    }
    public override void UpdateFlow(float _dt) {
        MenuManager.Instance.Update();
    }

}
