using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleFarmWork : Works
{
    [SerializeField] int foodArtisMiktari = 10;
    enum States { isYerine, waitGrow, controlGrow, wander, depoda }
    Transform foodDepo;
    States state;
    readonly WaitForSeconds stateAraDelayTime = new(3f); // 30 gibi bir sayý yapýlacak
    Vector3 isPoint;

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
        isPoint = isyeriKapi.GetComponentInParent<IsYerindekiler>().GetPoints()[0];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state == States.isYerine)
            {
                StateControl(States.waitGrow);
            }
        }
        if (other.transform == foodDepo)
        {
            if (state == States.controlGrow)
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
            case States.waitGrow:
                StartCoroutine(WaitForGrow());
                break;
            case States.controlGrow:
                ControlGrow();
                break;
            case States.wander:
                StartCoroutine(Wander());
                break;
            case States.depoda:
                StartCoroutine(Depoda());
                break;
        }
    }


    IEnumerator WaitForGrow()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        yield return stateAraDelayTime;
        StateControl(States.controlGrow);
    }

    void ControlGrow()
    {

        AgentGoToAndDO(isPoint, IsciAnimState.walkBag, 1, () => StartCoroutine(PickFurit()));
    }

    IEnumerator PickFurit()
    {
        StopAgent();
        SetAnimation(IsciAnimState.pickFruit);
        yield return stateAraDelayTime;
        while (Pools.FoodDepo.Count <= 0)
        {
            if (TimeTick())
            {
                SetAnimation(IsciAnimState.idleBag);
                timeTick = Random.Range(10f, 20f);
                TextControl.ins.AlertText("There is no grannary M'Lord!", color: Color.red, gecikme: 4f);
            }
            yield return null;
        }
        foodDepo = Pools.EnYakinDepoBul(isyeriKapi, Pools.FoodDepo);
        AgentGoToAndDO(foodDepo.position, IsciAnimState.walkBag, 1, () => StateControl(States.depoda));
    }

    IEnumerator Depoda()
    {
        StopAgent();
        SetAnimation(IsciAnimState.pickFruit);
        yield return stateAraDelayTime;
        Globals.ins.SetGlobals(GloabalResource.Food, foodArtisMiktari);
        AgentGoToAndDO(isyeriKapi.position, IsciAnimState.walk, 1, () => StateControl(States.wander));
    }

    IEnumerator Wander()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        Vector3 point = RandomNavmeshLocation(isyeriKapi.position, 20f);
        yield return stateAraDelayTime;
        AgentGoToAndDO(point, IsciAnimState.walk, 1, () => StartCoroutine(WanderWait()));
    }

    IEnumerator WanderWait()
    {
        StopAgent();
        SetAnimation(IsciAnimState.toSit);
        yield return stateAraDelayTime;
        SetAnimation(IsciAnimState.fromSit);
        yield return new WaitForSeconds(2f);
        AgentGoToAndDO(isyeriKapi.position, IsciAnimState.walk, 1, () => StartCoroutine(DelayABit()));
    }

    IEnumerator DelayABit()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        yield return stateAraDelayTime;
        StateControl(States.controlGrow);
    }
}
