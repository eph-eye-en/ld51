using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
