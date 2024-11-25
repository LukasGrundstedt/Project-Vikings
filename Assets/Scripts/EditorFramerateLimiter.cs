using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorFramerateLimiter : MonoBehaviour
{
    [SerializeField] private int fpsLimit = 120;


    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = fpsLimit;
    }
}