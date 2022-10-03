using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float secondsAllowed;
    [SerializeField]
    private List<ModuleController> stages;

    private int currStage;
    private System.DateTime endTime;

    void Start() {
        foreach (var s in stages) {
            s.OnFail.AddListener(FailLevel);
            s.OnSuccess.AddListener(NextStage);
        }
        stages[0].Attemptable = true;

        endTime = System.DateTime.Now.AddSeconds(secondsAllowed);
    }

    void Update() {
        if(System.DateTime.Now > endTime)
            FailLevel();
    }

    void NextStage() {
        stages[currStage].Attemptable = false;
        currStage++;
        if(currStage < stages.Count)
            stages[currStage].Attemptable = true;
        else
            WinLevel();
    }

    void FailLevel() {
        Debug.Log("FailLevel");
        //TODO: show fail screen before exiting
        BackToMenu(false);
    }

    void WinLevel() {
        Debug.Log("WinLevel");
        //TODO: show win screen before exiting
        BackToMenu(true);
    }

    public void BackToMenu(bool WinLose)
    {
        SceneManager.LoadScene(0);
        LevelSelectManager.HasPassedLastLevel = WinLose;
    }
}
