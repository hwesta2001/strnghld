using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] int w = 1280, h = 720, fps = 30;
    [SerializeField] bool screenSet = true;
    void Awake()
    {
        Application.targetFrameRate = fps;
        if (!screenSet) return;
        Screen.SetResolution(w, h, screenSet);
        DontDestroyOnLoad(gameObject);
    }

}
