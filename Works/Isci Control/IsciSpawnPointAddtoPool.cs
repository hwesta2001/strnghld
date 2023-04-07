using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsciSpawnPointAddtoPool : MonoBehaviour
{
    [SerializeField] MeshRenderer rend;
    [SerializeField] MeshFilter mf;
    void Start()
    {
        // RenderDisable();  // debug icin bunu disable yaptýk. Build zamaný aktif et.
        Pools.AddToPool(transform, Pools.isciSpawnPoints);
        Destroy(this);
    }

    void RenderDisable()
    {
        if (rend != null)
        {
            Destroy(rend);
            Destroy(mf);
        }
    }
}
