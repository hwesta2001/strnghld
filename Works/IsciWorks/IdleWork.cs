using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWork : Works
{
    enum State { toInn, wandering, walkingToRandomBuilding }
    State state;

    void OnEnable()
    {
        isyeriKapi = Globals.ins.Inn;
        Invoke(nameof(InitState), 1f);
    }
    void InitState() => SwitchState(State.toInn);

    void SwitchState(State _state)
    {
        state = _state;
        switch (state)
        {
            case State.toInn:
                ToInn();
                break;
            case State.wandering:
                StartCoroutine(Wandering());
                break;
            case State.walkingToRandomBuilding:
                StartCoroutine(WalkingToRandomBuilding());
                break;
        }
    }

    void ToInn()
    {
        float x = isyeriKapi.position.x + Random.Range(-2.6f, 2.6f);
        float z = isyeriKapi.position.z + Random.Range(-2.6f, 2.6f);
        Vector3 isyeriCivari = new(x, isyeriKapi.position.y, z);
        StopAllCoroutines();
        //SetAgentDestination(isyeriCivari);
        //SetAnimation(IsciAnimState.walkNotool);
        AgentGoToAndDO(isyeriCivari, IsciAnimState.walk, .5f, () => StartCoroutine(InitWander()));
    }


    IEnumerator InitWander()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
        yield return new WaitForSeconds(2f);
        SwitchState(State.wandering);
    }

    IEnumerator Wandering()
    {
        AgentGoToAndDO(Pools.InnRandomPoints[0].position, IsciAnimState.walkBag, 1f, StopAndIdle);
        yield return new WaitForSeconds(4f);
        AgentGoToAndDO(Pools.InnRandomPoints[1].position, IsciAnimState.walkKazma, 1f, StopAndIdle);
        yield return new WaitForSeconds(5f);
        AgentGoToAndDO(Pools.InnRandomPoints[2].position, IsciAnimState.run, 1f, StopAndIdle);
        yield return new WaitForSeconds(4f);
        AgentGoToAndDO(Pools.InnRandomPoints[3].position, IsciAnimState.walkBag, 1f, StopAndIdle);
        yield return new WaitForSeconds(4f);
        SwitchState(State.walkingToRandomBuilding);

    }

    void StopAndIdle()
    {
        StopAgent();
        SetAnimation(IsciAnimState.idleNoTool);
    }

    IEnumerator WalkingToRandomBuilding()
    {
        StopAndIdle();
        yield return new WaitForSeconds(.2f);
        Vector3 destination = RandomNavmeshLocation(isyeriKapi.position + isyeriKapi.forward * 10f, 25f);
        destination.y = 0;
        AgentGoToAndDO(destination, IsciAnimState.walk, 2f, ReReWalkingToRandomBuilding);
    }

    void ReReWalkingToRandomBuilding()
    {
        StartCoroutine(ReWalkingToRandomBuilding());
    }

    IEnumerator ReWalkingToRandomBuilding()
    {
        //Debug.Log("rewalking");
        StopAgent();
        float rand = Random.Range(0, 10);
        if (rand < 5f)
        {
            SetAnimation(IsciAnimState.idleNoTool);
            yield return new WaitForSeconds(Random.Range(15, 20));
            StartCoroutine(WalkingToRandomBuilding());
        }
        else
        {
            SetAnimation(IsciAnimState.toSit);
            yield return new WaitForSeconds(Random.Range(15, 20));
            SetAnimation(IsciAnimState.fromSit);
            yield return new WaitForSeconds(2f);
            StartCoroutine(WalkingToRandomBuilding());
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state != State.toInn) return;
            StopAllCoroutines();
            StartCoroutine(InitWander());
        }
    }

}
