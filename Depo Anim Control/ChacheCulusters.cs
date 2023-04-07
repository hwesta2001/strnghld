using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChacheCulusters : MonoBehaviour
{
    [Tooltip("Culuster Objelerinin Resource tipini seciniz.")]
    [SerializeField] GloabalResource resource = GloabalResource.Wood;
    [SerializeField] DepoAnimController depoAnimController;
    [SerializeField] float yükseklikRate = 0.11f;

    private void OnEnable()
    {
        ChacheAll();
    }

    [ContextMenu("Chache Culusters")]
    void ChacheAll()
    {

        if (depoAnimController == null)
        {
            depoAnimController = GetComponentInParent<DepoAnimController>();
        }

        Culuster[] culusters = gameObject.GetComponentsInChildren<Culuster>();
        switch (resource)
        {
            case GloabalResource.Gold:
                break;
            case GloabalResource.Wood:
                AddToCulusterList(depoAnimController.woodCulusters, culusters);
                break;
            case GloabalResource.Stone:
                AddToCulusterList(depoAnimController.stoneCulusters, culusters);
                break;
            case GloabalResource.Iron:
                AddToCulusterList(depoAnimController.ironCulusters, culusters);
                break;
            case GloabalResource.Food:
                break;
            case GloabalResource.Weath:
                break;
            case GloabalResource.Flour:
                AddToCulusterList(depoAnimController.flourCulusters, culusters);
                break;
            case GloabalResource.MaxPopulation:
                break;
            case GloabalResource.Population:
                break;
            case GloabalResource.Happines:
                break;
            case GloabalResource.LastEnum:
                break;
            default:
                break;
        }
    }

    void AddToCulusterList(List<Culuster> culusterList, Culuster[] culusters)
    {
        culusterList.Clear();
        foreach (var item in culusters)
        {
            culusterList.Add(item);
            item.riseRate = yükseklikRate;
        }
    }
}
