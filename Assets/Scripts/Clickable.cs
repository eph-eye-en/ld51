using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    public UnityEvent OnMouseDownEvent;
    public UnityEvent OnMouseUpEvent;

    void OnMouseDown() {
        OnMouseDownEvent.Invoke();
    }

    void OnMouseUp() {
        OnMouseUpEvent.Invoke();
    }
}
