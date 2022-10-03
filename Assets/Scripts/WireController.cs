using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    public int NumPoints;

    public Vector3 StartPos;
    public Vector3 EndPos;
    public float Height;
    public bool Unplugged;

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
            float y = Unplugged
                ? Mathf.Lerp(StartPos.y, EndPos.y, iScaled)
                    + Mathf.Sin(iScaled * Mathf.PI / 2) * Height * 2
                : Mathf.Lerp(StartPos.y, EndPos.y, iScaled)
                    + Mathf.Sin(iScaled * Mathf.PI) * Height;
            lineRenderer.SetPosition(i, new Vector3(
                Mathf.Lerp(StartPos.x, EndPos.x, iScaled),
                y,
                Mathf.Lerp(StartPos.z, EndPos.z, iScaled)
            ));
        }
    }

    public void SetMaterial(Material m) {
        //Start may not have assigned lineRenderer yet
        GetComponent<LineRenderer>().materials = new Material[]{ m };
    }
}
