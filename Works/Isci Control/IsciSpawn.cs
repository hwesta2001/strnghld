using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsciSpawn : MonoBehaviour
{

    [SerializeField] float isciCekmeZamani = 2f;
    bool tik = false;
    WaitForSeconds wfs;

    private void OnEnable()
    {
        wfs = new(isciCekmeZamani);
    }

    void Update()
    {
        if (Globals.ins.Population < Globals.ins.MaxPopulation)
        {
            if (tik) return;
            StopAllCoroutines();
            StartCoroutine(SpawnRoutine());
            tik = true;
        }
    }


    IEnumerator SpawnRoutine()
    {
        yield return wfs;
        IsciCagir();
        tik = false;
    }



    public void IsciCagir()
    {
        if (Globals.ins.Population < Globals.ins.MaxPopulation)
        {
            Instantiate(Globals.ins.isciPrefab, GetSpawnPoint(), Quaternion.identity);
            Globals.ins.SetGlobals(GloabalResource.Population, 1);
        }
    }

    Vector3 GetSpawnPoint()
    {
        if (Pools.isciSpawnPoints.Count <= 0) return Vector3.zero;
        int i = Random.Range(0, Pools.isciSpawnPoints.Count);

        Vector3 point = Pools.isciSpawnPoints[i].position;
        point.y = 0.01f; // ground seyiyesine ayarlamak için
        return point;

    }
}
