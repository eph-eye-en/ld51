using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void BackToMenu(bool WinLose)
    {
        SceneManager.LoadScene(0);
        LevelSelectManager.HasPassedLastLevel = WinLose;
    }

}
