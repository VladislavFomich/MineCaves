using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : Singleton<ApplicationManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    public void PauseGame()
    {
        Time.timeScale = 0f;
    }


    public void UnPauseGame()
    {
        Time.timeScale = 1f;
    }


    private void OnApplicationPause(bool pause)
    {
        Debug.LogWarning("Application Pause");
    }


    private void OnApplicationQuit()
    {
        Debug.LogWarning("Application Quit");
    }
}
