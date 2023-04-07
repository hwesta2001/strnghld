using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsciAl : MonoBehaviour
{
    bool canIsciAl;
    float timer = 0;
    [SerializeField] float kontrolPeriyodu = 2;
    Bina bina;

    void OnEnable()
    {
        canIsciAl = true;

    }


    void Update()
    {

        if (timer <=0)
        {
            PeriyodikKontrol();
            timer = kontrolPeriyodu;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }


    void PeriyodikKontrol()
    {
        if (!canIsciAl) return;
        Debug.Log("periyodik kontrol  " + gameObject.name);
    }



}
