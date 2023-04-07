using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmWheatWork : Works
{
    [SerializeField] // debug için
    private States state;
    Transform depo;
    bool barnOn;
    bool depoyaGidiyor;
    [SerializeField]
    int wheatArtisMiktari = 10;

    enum States { isYerine, tohumEk, waitforGrow, ekinTopla, depoya, depoda }
    void OnEnable()
    {
        isciControl = GetComponent<IsciControl>();
        binaGorevi = IsciGorevi.WheatFarmer;
        baslangicTik = false;
    }

    void Update()
    {
        IsyeriAktifKontrol();
        Basla();
        BarnControl();
    }

    private void BarnControl()
    {
        if (state != States.depoya) return;

        if (Pools.BarnDepo.Count > 0)
        {
            barnOn = true;
            Depoya();
        }
        else
        {
            barnOn = false;
        }
    }

    void Basla()
    {
        if (baslangicTik) return;
        baslangicTik = true;
        StateControl(States.isYerine);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state == States.isYerine)
            {
                StateControl(States.tohumEk);
            }
        }
        if (other.transform == depo)
        {
            if (state == States.depoya)
            {
                StateControl(States.depoda);
            }
        }
    }


    void StateControl(States _state)
    {
        state = _state;
        switch (_state)
        {
            case States.isYerine:
                IsyerineGit();
                break;
            case States.tohumEk:
                TohumEk();
                break;
            case States.waitforGrow:

                StartCoroutine(WaitGrow());
                break;
            case States.ekinTopla:
                StartCoroutine(EkinTopla());
                break;
            case States.depoya:
                depoyaGidiyor = false;
                Depoya();
                break;
            case States.depoda:

                StartCoroutine(Depoda());
                break;
        }
    }

    Vector3[] Pointler()
    {
        //IsYerindekiler iy = isyeri.GetComponentInParent<IsYerindekiler>();
        return isyeriKapi.GetComponentInParent<IsYerindekiler>().GetPoints();
    }



    private void TohumEk()
    {
        StopAgent();
        StartCoroutine(Ekim());
    }

    IEnumerator Ekim()
    {
        IsciAnimState animState = IsciAnimState.walkKazma;
        AgentGoToAndDO(Pointler()[0], animState, .1f, () => Isle(IsciAnimState.digPlant));
        yield return new WaitForSeconds(5f);
        AgentGoToAndDO(Pointler()[1], animState, .1f, () => Isle(IsciAnimState.digPlant));
        yield return new WaitForSeconds(6f);
        AgentGoToAndDO(Pointler()[2], animState, .1f, () => Isle(IsciAnimState.digPlant));
        yield return new WaitForSeconds(7f);
        AgentGoToAndDO(Pointler()[3], animState, .1f, () => Isle(IsciAnimState.digPlant));
        yield return new WaitForSeconds(8f);
        AgentGoToAndDO(Pointler()[4], animState, .4f, () => StateControl(States.waitforGrow));
        yield return null;

    }

    IEnumerator WaitGrow()
    {
        StopAgent();
        SetAnimation(IsciAnimState.toSit);
        yield return new WaitForSeconds(UnityEngine.Random.Range(8, 15));
        SetAnimation(IsciAnimState.fromSit);
        yield return new WaitForSeconds(3f);
        StateControl(States.ekinTopla);
    }
    IEnumerator EkinTopla()
    {
        IsciAnimState animState = IsciAnimState.walkBag;
        AgentGoToAndDO(Pointler()[0], animState, .1f, () => Isle(IsciAnimState.pickFruit));
        yield return new WaitForSeconds(5f);
        AgentGoToAndDO(Pointler()[1], animState, .1f, () => Isle(IsciAnimState.pickFruit));
        yield return new WaitForSeconds(6f);
        AgentGoToAndDO(Pointler()[2], animState, .1f, () => Isle(IsciAnimState.pickFruit));
        yield return new WaitForSeconds(7f);
        AgentGoToAndDO(Pointler()[3], animState, .1f, () => Isle(IsciAnimState.pickFruit));
        yield return new WaitForSeconds(8f);
        AgentGoToAndDO(Pointler()[4], animState, .4f, () => StateControl(States.depoya));
        yield return null;
    }

    void Isle(IsciAnimState animState)
    {
        StopAgent();
        SetAnimation(animState);
    }

    void Depoya()
    {
        if (depoyaGidiyor) return;
        if (barnOn)
        {
            depo = Pools.EnYakinDepoBul(isyeriKapi, Pools.BarnDepo);
            SetAgentDestination(depo.position);
            SetAnimation(IsciAnimState.walkBag);
            depoyaGidiyor = true;
        }
        else
        {
            SetAnimation(IsciAnimState.idleBag);
            StopAgent();
            TextControl.ins.AlertText("You must build a Barn M'Lord", 29, Color.gray, .1f);
        }

    }

    IEnumerator Depoda()
    {
        StopAgent();
        SetAnimation(IsciAnimState.attackBag);
        yield return new WaitForSeconds(UnityEngine.Random.Range(9, 12));
        Globals.ins.SetGlobals(GloabalResource.Weath, wheatArtisMiktari);
        StateControl(States.isYerine);
    }
}
