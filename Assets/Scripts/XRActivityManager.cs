using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class XRActivityManager : MonoBehaviour
{
    /// <summary>
    /// If application is paused, quit the app
    /// OpenXR stops rendering scene once paused
    /// This serves as a workaround to this issue
    /// </summary>
    /// <param name="pause"></param>
    void OnApplicationPause(bool pause)
    {
        if (pause) Application.Quit();
    }
    /// <summary>
    /// Returns to previous application on double click
    /// </summary>
    /// <param name="pause"></param>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("back button");
            Application.Quit();
        }
    }
}