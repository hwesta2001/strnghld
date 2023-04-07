using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillWork : Works
{

    [SerializeField] int flourArtisMiktari = 10;
    enum States { isYerine, waitForWheat, getWheat, working, gotoDepo, depoda }
    States state;

    Transform barnDepo;
    Transform flourDepo;

    readonly WaitForSeconds stateAraDelayTime = new(3f);
    readonly WaitForSeconds weathWaitDelayTime = new(3f);
    readonly WaitForSeconds getWheatDelayTime = new(3f);

    void OnEnable()
    {
        isciControl = GetComponent<IsciControl>();
        binaGorevi = IsciGorevi.AppleFarmer;
        baslangicTik = false;
        timeTick = 8f;
    }

    void Update()
    {
        IsyeriAktifKontrol();
        Basla();
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
                StateControl(States.waitForWheat);
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
                StopAllCoroutines();
                break;
            case States.waitForWheat:
                StartCoroutine(WaitForWheat());
                break;
            case States.getWheat:
                StartCoroutine(GetWheatFromBarn());
                break;
            case States.working:
                StartCoroutine(WorkingNow());
                break;
            case States.gotoDepo:
                StartCoroutine(GoToDepo());
                break;
            case States.depoda:
                StartCoroutine(Depoda());
                break;
        }
    }

    IEnumerator WaitForWheat()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idle);
        //yield return new WaitUntil(() => Pools.BarnDepo.Count > 0);
        yield return weathWaitDelayTime;
        StateControl(States.getWheat);
    }


    IEnumerator GetWheatFromBarn()
    {
        while (Pools.BarnDepo.Count <= 0)
        {
            SetAnimation(IsciAnimState.idle);
            TextControl.ins.AlertText("There is no Barn M'LORD!", color: Color.magenta, gecikme: 2f);
            yield return stateAraDelayTime;
        }
        yield return getWheatDelayTime;
        barnDepo = Pools.EnYakinDepoBul(isyeriKapi, Pools.BarnDepo);
        AgentGoToAndDO(barnDepo.position, IsciAnimState.walk, 1, () => StartCoroutine(BeckFromBarn()));
    }

    IEnumerator BeckFromBarn()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idle);
        //yield return new WaitUntil(() => Globals.ins.Weath >= 10);
        while (Globals.ins.Weath <= 10)
        {
            SetAnimation(IsciAnimState.idle);
            TextControl.ins.AlertText("No wheat M'LORD!", color: Color.yellow, gecikme: 2f);
            yield return stateAraDelayTime;
        }
        SetAnimation(IsciAnimState.attackBag);
        yield return getWheatDelayTime;
        Globals.ins.SetGlobals(GloabalResource.Weath, -10);
        AgentGoToAndDO(isyeriKapi.position, IsciAnimState.walkBag, 1, () => StateControl(States.working));
    }

    IEnumerator WorkingNow()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idle);
        yield return getWheatDelayTime;
        SetAnimation(IsciAnimState.attack);
        // windMilli döndür.
        yield return getWheatDelayTime;
        StateControl(States.gotoDepo);
    }

    IEnumerator GoToDepo()
    {
        StopAgent();
        while (Pools.depolar.Count <= 0)
        {
            SetAnimation(IsciAnimState.idleBag);
            TextControl.ins.AlertText("Stockpile is not avaliable.", color: Color.red);
            yield return getWheatDelayTime;
        }
        flourDepo = Pools.EnYakinDepoBul(isyeriKapi, Pools.depolar);
        AgentGoToAndDO(flourDepo.position, IsciAnimState.walkBag, 1, () => StateControl(States.depoda));
    }

    IEnumerator Depoda()
    {
        StopAgent();
        SetAnimation(IsciAnimState.attackBag);
        yield return getWheatDelayTime;
        Globals.ins.SetGlobals(GloabalResource.Flour, 1);
        StateControl(States.isYerine);
    }
}
