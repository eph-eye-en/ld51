using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public Color LockedColour;
    public Color CurrentColour;
    public Color PastColour;
    public List<LevelNodeScript> LevelNodes;
    private int levelnumber = 0;
    public int LevelNumber { 
        set {
            levelnumber = value;
            UpdateNodes();
        }
        get {
            return levelnumber;
        }
    }
    public static bool HasPassedLastLevel = false;


    void Start()
    {
        LevelNumber = PlayerPrefs.GetInt("LevelNumb");

        if (LevelNodes.Count == 0) {
            Debug.LogError("No Nodes");
        }
        if (HasPassedLastLevel)
        {
            HasPassedLastLevel = false;
            LevelNumber += 1;
        }
        PlayerPrefs.SetInt("LevelNumb", LevelNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void LoadScene(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);

    }
}
