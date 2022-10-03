using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelSelectManager : MonoBehaviour
{
    public Color LockedColour;
    public Color CurrentColour;
    public Color PastColour;
    public List<LevelNodeScript> LevelNodes;
    public List<GameObject> LevelBriefs;
    private int levelnumber = 0;
    public int LevelNumber { 
        set {
            levelnumber = value;
            PlayerPrefs.SetInt("LevelNumb", LevelNumber);
            UpdateNodes();
        }
        get {
            return levelnumber;
        }
    }
    public static bool HasPassedLastLevel = false;

    public Transform DisplayTransform;
    LevelNodeScript ChosenLevel;

    int LevelDisplayState = 0;

    void Start()
    {
        for (int i = 0; i < LevelBriefs.Count; i++)
        {
            LevelBriefs[i].SetActive(false);
        }

        LevelNumber = PlayerPrefs.GetInt("LevelNumb");

        if (LevelNodes.Count == 0) {
            Debug.LogError("No Nodes");
        }
        if (HasPassedLastLevel)
        {
            HasPassedLastLevel = false;
            LevelNumber += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            CheckLevelSelect();

        //if(LevelDisplayState == 1)
        //{
        //    ShowLevelMenu();
        //}else if (LevelDisplayState == 2)
        //{
        //    HideLevelMenu();
        //}

        
    }

    void UpdateNodes() {
        for (int i = 0; i < LevelNumber; i++) {
            LevelNodes[i].GetComponent<SpriteRenderer>().color = PastColour;
            LevelNodes[i].IsCurrentLevel = false;
        }
        
        LevelNodes[LevelNumber].GetComponent<SpriteRenderer>().color = CurrentColour;
        LevelNodes[LevelNumber].IsCurrentLevel = true;

        for (int i = LevelNumber + 1; i < LevelNodes.Count; i++)
        {
            LevelNodes[i].GetComponent<SpriteRenderer>().color = LockedColour;
            LevelNodes[i].IsCurrentLevel = false;
        }
    }

    void CheckLevelSelect() {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        Physics.Raycast(mouseRay, out hitInfo);
        LevelNodeScript nodeScript = hitInfo.transform.GetComponent<LevelNodeScript>();

        if (LevelNodes.Contains(nodeScript) && nodeScript != null)
        {
            Debug.LogWarning(Camera.main);

            if (LevelBriefs.Count >= LevelNodes.IndexOf(nodeScript) && LevelBriefs[LevelNodes.IndexOf(nodeScript)] != null && LevelNodes.IndexOf(nodeScript)<=LevelNumber)
                LevelBriefs[LevelNodes.IndexOf(nodeScript)].SetActive(true);
        }
        else
        {
            Debug.Log("Nothing Hit");
            if (!EventSystem.current.IsPointerOverGameObject())
            {

                for (int i = 0; i < LevelBriefs.Count; i++)
                {
                    LevelBriefs[i].SetActive(false);
                }
            }
        }
    }

    void ShowLevelMenu()
    {
        ChosenLevel.Bomb.transform.position = LerpToPosition(ChosenLevel.BombLocation.position, DisplayTransform.position, 0.5f);
        ChosenLevel.Bomb.transform.localRotation = LerpToRotation(ChosenLevel.Bomb.transform.localRotation, Quaternion.Euler(new Vector3(0, -90, 90)), 0.5f);
    }

    void HideLevelMenu()
    {
        ChosenLevel.Bomb.transform.position = LerpToPosition(DisplayTransform.position, ChosenLevel.BombLocation.position, 0.5f);
        ChosenLevel.Bomb.transform.localRotation = LerpToRotation(ChosenLevel.Bomb.transform.localRotation, Quaternion.Euler(new Vector3(0, -90, 90)), 0.5f);

        Debug.LogWarning(ChosenLevel.Bomb.transform.position);
        Debug.Log(ChosenLevel.BombLocation.position);

        if(LevelDisplayState==0)
        {
            ChosenLevel.Bomb.transform.parent = ChosenLevel.BombLocation;
            ChosenLevel = null;
        }
    }

    private float TimeElapsed;
    Vector3 LerpToPosition(Vector3 From,Vector3 To,float Duration)
    {
        Vector3 Pos;
        if (TimeElapsed < Duration)
        {
            Pos = Vector3.Lerp(From, To, TimeElapsed / Duration);
        }
        else
        {
            Debug.LogWarning("Finish Lerp");
            Pos = To;
            LevelDisplayState = 0;
        }
        return Pos;
    }

    Quaternion LerpToRotation(Quaternion From, Quaternion To, float Duration)
    {
        Quaternion Rot;
        if (TimeElapsed < Duration)
        {
            Rot = Quaternion.Lerp(From, To, TimeElapsed / Duration);
            TimeElapsed += Time.deltaTime;
        }
        else
        {
            Rot = To;
        }
        return Rot;
    }

    public void LoadScene(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);

    }
}
