using UnityEngine;
using System.Collections;

public abstract class Flow {

    public abstract void InitializeFlow();
    public abstract void UpdateFlow(float _dt);
    public abstract void FixedUpdateFlow(float _fdt);
    public abstract void Finish();

}
