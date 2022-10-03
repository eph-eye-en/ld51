using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleController : MonoBehaviour
{
    public Vector2Int Size;
    public Vector2Int Position {
        get {
            return Vector2Int.RoundToInt(transform.position);
        }
        set {
            transform.position = (Vector2)value;
        }
    }

    public bool Attemptable;
    public UnityEvent OnSuccess;
    public UnityEvent OnFail;
}
