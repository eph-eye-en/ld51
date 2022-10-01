using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    Camera MainCam;
    public float ScreenTolerance;
    public float CamMoveSpeed;

    void Start()
    {
        MainCam = gameObject.GetComponent<Camera>();    
    }

    // Update is called once per frame
    void Update()
    {
        float XDirection = 0;
        float YDirection = 0;
        Vector3 MousePixelPos = Input.mousePosition;

        Vector2 MouseScreenPos = new Vector2(MousePixelPos.x / Screen.width, MousePixelPos.y / Screen.height);
        if (MouseScreenPos.x < ScreenTolerance || MouseScreenPos.x > 1 - ScreenTolerance)
        {
            XDirection = (MouseScreenPos.x - 0.5f)/Mathf.Abs(MouseScreenPos.x - 0.5f);
        }

        if (MouseScreenPos.y < ScreenTolerance || MouseScreenPos.y > 1 - ScreenTolerance)
        {
            YDirection = (MouseScreenPos.y - 0.5f) / Mathf.Abs(MouseScreenPos.y - 0.5f);
        }

        MainCam.transform.position += new Vector3(XDirection * CamMoveSpeed * Time.deltaTime, 0, YDirection * CamMoveSpeed * Time.deltaTime);

    }
}
