using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshSurfaceInit : MonoBehaviour
{
    [SerializeField] bool buildOnEnable = false;
    [SerializeField] GameObject[] disabledObjectsAfterNavMeshGeneration;
    private void OnEnable() // or onbuildDone 
    {
        if (buildOnEnable)
        {
            BuildSurface();
        }

    }

    [ContextMenu("Build NavMesh")]
    public void BuildSurface()
    {
        NavMesh.RemoveAllNavMeshData();
        GetComponent<NavMeshSurface>().BuildNavMesh();
        DestroyGFX();
    }

    void DestroyGFX()
    {
        if (disabledObjectsAfterNavMeshGeneration.Length <= 0) return;
        foreach (var item in disabledObjectsAfterNavMeshGeneration)
        {
            Destroy(item);
        }
    }
}
