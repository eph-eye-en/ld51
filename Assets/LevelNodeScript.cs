using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNodeScript : MonoBehaviour
{
    public float MaxPulse;
    public float MinPulse;
    public float PulseTime = 1f;
    private bool PulseUp;

    public bool IsCurrentLevel;


    void Update()
    {
        Pulse();
    }

    void Pulse()
    {
        float PulseSpeed = (MaxPulse - MinPulse) / PulseTime;


        if (isActiveAndEnabled && IsCurrentLevel) {
            if (PulseUp) {
                transform.localScale += new Vector3(PulseSpeed * Time.deltaTime, PulseSpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.localScale -= new Vector3(PulseSpeed * Time.deltaTime, PulseSpeed * Time.deltaTime, 0);
            }

            if (transform.localScale.x >= MaxPulse)
                PulseUp = false;
            else if (transform.localScale.x <= MinPulse)
                PulseUp = true;
        }
    }
}
