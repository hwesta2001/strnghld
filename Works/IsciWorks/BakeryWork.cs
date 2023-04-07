using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeryWork : Works
{

    enum States { isYerine, waitForBake, toDepoOrGranary, depoda }
    States state;
    private Transform depo;
    readonly WaitForSeconds bakeDelayTime = new(20f);
    bool toFlour;
    void OnEnable()
    {
        isciControl = GetComponent<IsciControl>();
        binaGorevi = IsciGorevi.AppleFarmer;
        baslangicTik = false;
        timeTick = 4f;
        toFlour = true;
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
    void StateControl(States _state)
    {
        state = _state;
        switch (_state)
        {
            case States.isYerine:
                IsyerineGit();
                StopAllCoroutines();
                break;
            case States.waitForBake:
                StartCoroutine(WaitForBake());
                break;
            case States.toDepoOrGranary:
                if (toFlour)
                {
                    StartCoroutine(GetFlour());
                }
                else
                {
                    StartCoroutine(ToGranary());
                }
                break;
            case States.depoda:
                StartCoroutine(Depoda());
                break;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state == States.isYerine)
            {
                StateControl(States.waitForBake);
            }
        }

    }
    IEnumerator WaitForBake()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        yield return bakeDelayTime;
        StateControl(States.toDepoOrGranary);
    }


    IEnumerator GetFlour()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);

        while (Globals.ins.Flour < 1)
        {
            SetAnimation(IsciAnimState.idle);
            TextControl.ins.AlertText("There is not enough Flour M'LORD!", color: Color.white, gecikme: 2f);
            yield return new WaitForSeconds(timeTick);
        }


        yield return bakeDelayTime;

        depo = Pools.EnYakinDepoBul(isyeriKapi, Pools.depolar);
        AgentGoToAndDO(depo.position, IsciAnimState.walk, 1, () => StateControl(States.depoda));
    }
    IEnumerator ToGranary()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        yield return new WaitForSeconds(timeTick * .5f);
        depo = Pools.EnYakinDepoBul(isyeriKapi, Pools.FoodDepo);
        AgentGoToAndDO(depo.position, IsciAnimState.walk, 1, () => StateControl(States.depoda));

    }

    IEnumerator Depoda()
    {
        StopAgent();
        SetAnimation(IsciAnimState.attackBag);
        yield return new WaitForSeconds(timeTick);
        if (toFlour)
        {
            Globals.ins.SetGlobals(GloabalResource.Flour, -1);
            toFlour = false;
        }
        else
        {
            Globals.ins.SetGlobals(GloabalResource.Food, 10);
            toFlour = true;
        }
        StateControl(States.isYerine);
    }

}
