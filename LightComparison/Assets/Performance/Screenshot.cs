using UnityEngine;
using System.Collections.Generic;
using System;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private string path;
    private string fileName;
    [SerializeField]
    [Range(1, 5)]
    private int size = 1;
    [SerializeField]
    private int count = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Screenshot");
            fileName = "screenshot_";
            fileName += count.ToString() + ".png";
            ++count;
            ScreenCapture.CaptureScreenshot(path + fileName, size);
        }
    }
}