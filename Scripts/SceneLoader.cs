using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] List<NavMeshSurfaceInit> navMeshInit = new();
    [SerializeField] List<GameObject> InitGameObjects = new();
    [SerializeField] float StartDelay = 2f;

    void Awake()
    {
        Pools.ClearAllPools();
        GlobalVeriler.GAMESTATE = GameGlobalStates.WaitingForInit;
        foreach (GameObject item in InitGameObjects)
        {
            Instantiate(item);
        }
        foreach (NavMeshSurfaceInit item in GetComponentsInChildren<NavMeshSurfaceInit>())
        {
            navMeshInit.Add(item);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        foreach (NavMeshSurfaceInit item in navMeshInit)
        {
            item.BuildSurface();
        }

        yield return new WaitForSeconds(StartDelay);
        GlobalVeriler.GAMESTATE = GameGlobalStates.Start;
    }
}
