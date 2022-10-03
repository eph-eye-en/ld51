using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonModule : ModuleController
{
    [SerializeField]
    private ButtonHeadController ButtonHead;

    public void Press() {
        if(Attemptable)
            OnSuccess.Invoke();
        else
            OnFail.Invoke();
    }

    public void Release() {
    }
}
