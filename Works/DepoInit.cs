using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepoInit : MonoBehaviour
{
    [SerializeField] Depo depo;
    BuildingControl buildingControl;


    void OnEnable()
    {
        if (!depo.kapi.gameObject.activeSelf)
        {
            Debug.Log(gameObject.name + " objesine kapi transformunu ekleyiniz");
        }
        buildingControl = GetComponent<BuildingControl>();
        if (buildingControl == null)
        {
            if (GlobalVeriler.GAMESTATE != GameGlobalStates.WaitingForInit)
            {
                SetDepoPool();
            }
            else
            {
                StartCoroutine(DelayedSepoSetter());
            }
        }
        else
        {
            buildingControl.KurulumSonrasi += SetDepoPool;
        }
    }


    IEnumerator DelayedSepoSetter()
    {

        while (GlobalVeriler.GAMESTATE == GameGlobalStates.WaitingForInit)
        {
            yield return new WaitForSeconds(.1f);
        }
        SetDepoPool();
    }

    private void SetDepoPool()
    {
        Debug.Log("SetDepoPool");
        switch (depo.depoCinsi)
        {
            case DepoCinsi.Resource:
                Pools.AddToPool(depo, Pools.depolar);
                break;
            case DepoCinsi.Food:
                Pools.AddToPool(depo, Pools.FoodDepo);
                break;
            case DepoCinsi.Barn:
                Pools.AddToPool(depo, Pools.BarnDepo);
                break;
            case DepoCinsi.Armory:
                Pools.AddToPool(depo, Pools.Armory);
                break;
            default:
                break;
        }

        if (buildingControl == null) return;
        buildingControl.KurulumSonrasi -= SetDepoPool;
    }

    void OnDisable()
    {
        switch (depo.depoCinsi)
        {
            case DepoCinsi.Resource:
                Pools.RemoveFromPool(depo, Pools.depolar);
                break;
            case DepoCinsi.Food:
                Pools.RemoveFromPool(depo, Pools.FoodDepo);
                break;
            case DepoCinsi.Barn:
                Pools.RemoveFromPool(depo, Pools.BarnDepo);
                break;
            case DepoCinsi.Armory:
                Pools.RemoveFromPool(depo, Pools.Armory);
                break;
            default:
                break;
        }
    }
}
