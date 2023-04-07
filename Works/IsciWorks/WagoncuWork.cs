using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagoncuWork : Works
{
    public Transform wagon;
    public bool started;
    public WagonControl wagonControl;
    public LocalResourceSayac sayac;

    enum States { wagonAl, kaynakBekle, kaynakYukle, depoyaGit, kaynakIndir, isyerineGel };
    States state;
    bool kaynakKontrolEt;
    int adet;
    Transform depo;

    void OnEnable()
    {
        started = false;
        isciControl = GetComponent<IsciControl>();
    }


    void Update()
    {
        IsyeriAktifKontrol();
        Basla();
        KaynakKontrol();
        if (state == States.kaynakYukle)
        {
            if (wagonControl.GetDoldu())
            {
                StateControl(States.depoyaGit);
            }
        }

        if (state == States.kaynakIndir)
        {
            if (!wagonControl.GetDoldu())
            {
                Globals.ins.SetGlobals(GloabalResource.Stone, adet);
                StateControl(States.isyerineGel);
            }
        }
    }



    void Basla()
    {
        if (!started) return;
        adet = wagonControl.GetMaxMaddeSayisi();
        StateControl(States.wagonAl);
    }

    void KaynakKontrol()
    {
        if (!kaynakKontrolEt) return;
        if (sayac.adet >= wagonControl.GetMaxMaddeSayisi())
        {
            StateControl(States.kaynakYukle);
        }
    }



    void StateControl(States _state)
    {
        state = _state;
        switch (state)
        {
            case States.wagonAl:
                kaynakKontrolEt = false;
                WagonAl();
                break;
            case States.kaynakBekle:
                kaynakKontrolEt = true;
                KaynakBekle();
                break;
            case States.kaynakYukle:
                kaynakKontrolEt = false;
                AgentGoToAndDO(isyeriKapi.position, IsciAnimState.wagonWalk, 1.6f, () => KaynakYukle());
                break;
            case States.depoyaGit:
                kaynakKontrolEt = false;
                DepoyaGit();
                break;
            case States.kaynakIndir:
                kaynakKontrolEt = false;
                KaynakIndir();
                break;
            case States.isyerineGel:
                kaynakKontrolEt = false;
                IsyerineGel();
                break;


            default:
                break;
        }
    }



    void WagonAl()
    {
        started = false;
        SetAgentDestination(wagon.position);
        SetAnimation(IsciAnimState.walkKazma);
    }
    void WagonSet()
    {

        wagonControl.WagonKasaFallow(transform, true);
        Vector3 point = isyeriKapi.position + isyeriKapi.forward * 8f;
        point.y = 0;
        AgentGoToAndDO(point, IsciAnimState.wagonWalk, 1.6f, () => StateControl(States.kaynakBekle));

    }

    void KaynakBekle()
    {

        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
    }

    void KaynakYukle()
    {
        StopAgent();
        SetAnimation(IsciAnimState.attack);
        if (adet > 0)
        {
            wagonControl.WagonKasaDoldur();
            sayac.SetLocalStone(-adet);
        }
    }


    void DepoyaGit()
    {
        depo = Pools.EnYakinDepoBul(isyeriKapi, Pools.depolar);
        SetAgentDestination(depo.position);
        SetAnimation(IsciAnimState.wagonWalk);
    }


    void KaynakIndir()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idle);
        wagonControl.WagonKasaBosalt();
    }


    void IsyerineGel()
    {
        SetAgentDestination(isyeriKapi.position);
        SetAnimation(IsciAnimState.wagonWalk);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.transform == wagon)
        {
            if (state == States.wagonAl)
            {
                WagonSet();

            }
        }

        if (other.transform == depo)
        {
            if (state == States.depoyaGit)
            {
                StateControl(States.kaynakIndir);
            }
        }

        if (other.transform == isyeriKapi)
        {
            if (state == States.isyerineGel)
            {
                StateControl(States.kaynakBekle);
            }
        }
    }


}