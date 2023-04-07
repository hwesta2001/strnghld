using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class EditorSettings : MonoBehaviour
{
    public bool frameRateChane;
    void Awake()
    {
        if (frameRateChane)
        {

            Application.targetFrameRate = 30;
        }
    }
}
