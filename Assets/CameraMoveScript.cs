using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    Camera MainCam;
    public float ScreenTolerance;
    public float CamMoveSpeed;

    public Vector2 MaxBounds;
    public Vector2 MinBounds;

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

        Vector2 NewPos = new Vector2(MainCam.transform.position.x + (XDirection * CamMoveSpeed * Time.deltaTime), MainCam.transform.position.z + (YDirection * CamMoveSpeed * Time.deltaTime));

        if(NewPos.x < MinBounds.x || NewPos.x > MaxBounds.x)
        {
            NewPos.x = MainCam.transform.position.x;
        }
        if (NewPos.y < MinBounds.y || NewPos.y > MaxBounds.y)
        {
            NewPos.y = MainCam.transform.position.z;
        }

        MainCam.transform.position = new Vector3(NewPos.x,transform.position.y,NewPos.y);

    }

    private void OnDrawGizmos()
    {
        float PosY = transform.position.y;
        Gizmos.DrawLine(new Vector3(MinBounds.x, PosY, MinBounds.y), new Vector3(MinBounds.x, PosY, MaxBounds.y));
        Gizmos.DrawLine(new Vector3(MinBounds.x, PosY, MinBounds.y), new Vector3(MaxBounds.x, PosY, MinBounds.y));
        Gizmos.DrawLine(new Vector3(MaxBounds.x, PosY, MaxBounds.y), new Vector3(MaxBounds.x, PosY, MinBounds.y));
        Gizmos.DrawLine(new Vector3(MaxBounds.x, PosY, MaxBounds.y), new Vector3(MinBounds.x, PosY, MaxBounds.y));
    }

}
