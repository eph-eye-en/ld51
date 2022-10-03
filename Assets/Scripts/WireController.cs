using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    public int NumPoints;

    public Vector3 StartPos;
    public Vector3 EndPos;
    public float Height;

    private LineRenderer lineRenderer;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        RegeneratePoints();
    }

    public void RegeneratePoints() {
        lineRenderer.positionCount = NumPoints;
        for(int i = 0; i < NumPoints; i++) {
            // NumPoints - 1 because the end points are inclusive
            float iScaled = (float)i / (NumPoints - 1);
            lineRenderer.SetPosition(i, new Vector3(
                Mathf.Lerp(StartPos.x, EndPos.x, iScaled),
                Mathf.Lerp(StartPos.y, EndPos.y, iScaled)
                    + Mathf.Sin(iScaled * Mathf.PI) * Height,
                Mathf.Lerp(StartPos.z, EndPos.z, iScaled)
            ));
        }
    }
}
