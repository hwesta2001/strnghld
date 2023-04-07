using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Culuster : MonoBehaviour
{
    public List<GameObject> blocks = new();
    public Transform anablock;
    public int anaBlockYukseltiAdedi;
    [HideInInspector]
    public float riseRate = 0.11f;
}
