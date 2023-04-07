using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiBehaviour", menuName = "AiSystem", order = 1)]
public class AiBehaviour : ScriptableObject
{
    public int GlobalStartTime = 10;
    public List<AiSteps> aiSteps = new List<AiSteps>();
}

