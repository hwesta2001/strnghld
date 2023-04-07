using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hovel : MonoBehaviour
{
    BuildingControl building;
    bool tik;
    private void OnEnable()
    {
        tik = false;
        building = GetComponent<BuildingControl>();
        building.KurulumSonrasi += HovelPopSet;
    }


    void HovelPopSet()
    {
        if (!building.isBuilt) return;
        if (tik) return;
        tik = true;
        Debug.Log("tetikle");
        Globals.ins.SetGlobals(GloabalResource.MaxPopulation, 2);
        building.KurulumSonrasi -= HovelPopSet;
    }
}
