using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshInitMeshDelete : MonoBehaviour
{

    MeshRenderer rend;
    MeshFilter meshFilter;

    void OnEnable()
    {
        rend = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        StartCoroutine(DelayedInit());
    }

    IEnumerator DelayedInit()
    {
        yield return new WaitForEndOfFrame();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        Destroy(rend);
        Destroy(meshFilter);
    }
}
