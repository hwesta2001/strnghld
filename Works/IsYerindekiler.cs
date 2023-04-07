using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsYerindekiler : MonoBehaviour
{
    public List<Transform> isPointleri = new List<Transform>();


    public Vector3[] GetPoints()
    {
        Vector3[] vec = new Vector3[isPointleri.Count];
        for (int i = 0; i < isPointleri.Count; i++)
        {
            vec[i] = isPointleri[i].position;
        }
        return vec;
    }
}
