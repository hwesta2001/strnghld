using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagonActivate : MonoBehaviour
{
    [SerializeField] BuildingControl building;
    [SerializeField] GameObject wagon;

    void OnEnable()
    {
        building.KurulumSonrasi += WagonActive;
        wagon.SetActive(false);
    }

    void WagonActive()
    {
        wagon.SetActive(true);
        wagon.GetComponent<WagonControl>().started = true;
        building.KurulumSonrasi -= WagonActive;
    }


}
