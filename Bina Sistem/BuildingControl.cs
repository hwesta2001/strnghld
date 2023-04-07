using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class BuildingControl : MonoBehaviour
{
    public Bina bina;
    public IsciGorevi gorev = IsciGorevi.NonWorker;

    public event Action KurulumSonrasi;
    // binadaki scripte bu evente "building.KurulumSonrasi += HovelPopSet;" benzer ekleme yapýlýr.
    // böylece diðer script çalýþmaya bina kurulduktan sonra baþlar.


    //[HideInInspector]
    public bool needWorker;
    [HideInInspector]
    public bool isBuilt;

    float timer = 1;
    int kacIsci;
    List<GameObject> isciler = new List<GameObject>();
    Transform kapi;

    private void OnEnable()
    {
        needWorker = false;
    }
    void OnDisable()
    {
        kapi.RemoveFromPool(Pools.buildingKapilar);
    }

    //public void WaitForInsaa()
    //{
    //    Invoke(nameof(Kuruldu), bina.buildTime);
    //}


    public void Kuruldu()
    {
        if (isBuilt) return;
        isBuilt = true;
        KurulumSonrasi?.Invoke();

        kapi = transform.Find("kapi");
        kapi.AddToPool(Pools.buildingKapilar);

        if (gorev == IsciGorevi.Idle || gorev == IsciGorevi.NonWorker) return; // nonWorker görevi hovel vb içindir!
        needWorker = true;
        this.AddToPool(Pools.Binalar);
        kacIsci = bina.isciSayisi;
        kacIsci = Mathf.Clamp(kacIsci, 0, bina.isciSayisi);

    }

    void Update()
    {
        if (!isBuilt) return;

        if (timer <= 0)  // timer çünkü her updatede olmasýn performans???
        {
            if (kacIsci > 0) needWorker = true;
            KontrolWorker();
            GetWorker();
            timer = UnityEngine.Random.Range(2f, 4f);
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }


    private void KontrolWorker()
    {
        if (isciler.Count <= 0) return;
        for (int i = 0; i < isciler.Count; i++)
        {
            if (isciler[i].activeInHierarchy) return;
            isciler.Remove(isciler[i]);
            kacIsci++;
            kacIsci = Mathf.Clamp(kacIsci, 0, bina.isciSayisi);
        }
    }

    void GetWorker()
    {
        if (!needWorker) return;
        if (Pools.Isciler.Count <= 0) return;
        for (int i = 0; i < Pools.Isciler.Count; i++)
        {
            if (Pools.Isciler[i].isci.gorev == IsciGorevi.Idle)
            {
                IsciControl ic = Pools.Isciler[i];
                ic.IsciGorevSet(gorev, transform);
                if (!isciler.Contains(ic.gameObject))
                {
                    isciler.Add(ic.gameObject);
                }
                kacIsci -= 1;
                if (kacIsci <= 0)
                {
                    needWorker = false;
                }
                else
                {
                    needWorker = true;
                }
                return;
            }
            else
            {
                needWorker = true;
            }
        }
    }
}
