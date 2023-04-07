using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inn : MonoBehaviour
{
    [SerializeField] List<Transform> pointsToAdd = new();
    void Start()
    {
        foreach (Transform item in pointsToAdd)
        {
            item.AddToPool(Pools.InnRandomPoints);
        }
    }

}
