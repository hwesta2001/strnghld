// Bu WagonControl ile tüm wagondaki scritplere eriþip kontrol edilecek
// yani statede sadece bu WagonControl e referance verilince tüm kontrol tek
// buradan saðlanabilecek. 


using System.Collections;
using UnityEngine;

public class WagonControl : MonoBehaviour
{
    [SerializeField] KasaFallow kasaFallow;
    [SerializeField] KasaDoldur kasaDoldur;

    public bool started;

    Transform isyeri;
    WagoncuWork wagoncuWork;
    bool needWorker;
    float timer;

    void OnEnable()
    {
        isyeri = transform.parent;
        needWorker = true;
    }

    public int GetMaxMaddeSayisi()
    {
        return kasaDoldur.SetMaxMaddeSayisi();
    }
    public bool GetDoldu()
    {
        return kasaDoldur.GetDoldu();
    }
    public void WagonKasaFallow(Transform tr, bool fallow)
    {
        kasaFallow.KasaTarget(tr, fallow);
    }

    public void WagonKasaDoldur()
    {
        kasaDoldur.KasayiDoldur();
    }

    public void WagonKasaBosalt()
    {
        if (!kasaDoldur.maddeAnim) return;
        kasaDoldur.maddeAnim.FullBosalt();
    }


    void Update()
    {
        if (!started) return;
        if (timer <= 0)
        {
            GetWorker();
            timer = 1;
        }
        else
        {
            timer -= Time.deltaTime;
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
                ic.IsciGorevSet(IsciGorevi.Wagoncu, transform.parent);
                StartCoroutine(GetWagoncuWork(ic));
                needWorker = false;
                return;
            }
            else
            {
                needWorker = true;
            }
        }
    }

    IEnumerator GetWagoncuWork(IsciControl ic)
    {
        yield return new WaitForEndOfFrame();
        wagoncuWork = ic.GetComponent<WagoncuWork>();
        wagoncuWork.wagon = transform;
        wagoncuWork.wagonControl = this;
        wagoncuWork.sayac = transform.parent.GetComponent<LocalResourceSayac>();
        //transform.parent = null;
        wagoncuWork.started = true;

    }

}
