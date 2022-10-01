using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHeadController : MonoBehaviour
{
    public Transform Mesh;
    public float UnpressedY;
    public float PressedY;

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    public void Press() {
        Vector3 pos = Mesh.localPosition;
        pos.y = PressedY;
        Mesh.localPosition = pos;
        OnPressed.Invoke();
    }

    public void Release() {
        Vector3 pos = Mesh.localPosition;
        pos.y = UnpressedY;
        Mesh.localPosition = pos;
        OnReleased.Invoke();
    }
}
