using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Works : MonoBehaviour
{
    protected IsciGorevi binaGorevi;
    protected Transform isyeriKapi;
    public IsciControl isciControl;
    protected bool baslangicTik;
    protected float timeTick = 3f;
    protected IsciAnimState isciAnimStateCurrent;

    protected void IsyeriAktifKontrol()  // bunu tüm updatelere mecbur ekle
    {
        if (TimeTick())
        {
            if (isyeriKapi.gameObject.activeInHierarchy) return;
            Debug.Log("isyeri kontrol edildi ve " + isyeriKapi.gameObject.name + " olmadýðý görüldi idleWork e dönülüyor.");
            isciControl.IsciGorevSet(IsciGorevi.Idle);

        }
    }



    public void IsyeriKapi(Transform tr)
    {
        isyeriKapi = tr.Find("kapi");
        if (isyeriKapi == null) Debug.Log("Bu iþyerinde kapý objesi yok : " + tr.name); //debug 
    }

    public void Isyeri(Transform tr)
    {
        isyeriKapi = tr;
    }

    protected void IsyerineGit()
    {
        SetAgentDestination(isyeriKapi.position);
        SetAnimation(IsciAnimState.run, 1.2f);
    }

    protected void SetAgentDestination(Vector3 point)
    {
        isciControl.AgentToDest(point);
    }

    public delegate void Afunc();

    protected void AgentGoToAndDO(Vector3 point, IsciAnimState? animState = null, float? distance = null, Afunc func = null)
    {
        //AgentStop();
        SetAgentDestination(point);
        SetAnimation(animState ?? IsciAnimState.walkKazma);
        Afunc _f = func is null ? Extantions.FisEmpty : func;
        StartCoroutine(AgentGoTo(point, distance ?? 1.2f, _f));
    }

    IEnumerator AgentGoTo(Vector3 point, float distance, Afunc func)
    {
        yield return new WaitUntil(() => (HedefDistanceControl(point, distance)) || AgentDistanceControl());
        func();
    }

    protected bool AgentDistanceControl()
    {
        return HedefDistanceControl(isciControl.agent.destination, isciControl.agent.stoppingDistance);
    }

    protected bool HedefDistanceControl(Vector3 hedef, float? mesafe = null)
    {
        //Vector3 pos = new(transform.position.x, 0, transform.position.z);
        Vector3 pos = transform.position;

        float _mesafe = mesafe ?? 1.4f;
        float dist = Vector3.Distance(pos, hedef);
        return dist <= _mesafe;

    }



    protected void SetAnimation(IsciAnimState animState, float? speed = null)
    {
        if (isciAnimStateCurrent == animState) return;
        isciAnimStateCurrent = animState;
        isciControl.AnimSet(animState, speed ?? 1f);
        if (animState is IsciAnimState.idle or IsciAnimState.idleBag or IsciAnimState.idleNoTool or IsciAnimState.idleWood)
        {
            StopAgent();
        }
    }

    protected void StopAgent()
    {
        isciControl.AgentStop();
    }


    protected bool AgentHasNoPath(Vector3 tr)
    {
        return isciControl.AgentHasNoPath(tr);
    }

    protected Vector3 RandomNavmeshLocation(Vector3 center, float radius)
    {
        return isciControl.RandomNavmeshLocation(center, radius);
    }


    protected bool TimeTick()
    {
        timeTick -= Time.deltaTime;
        if (timeTick < 0)
        {
            timeTick = 5f;
            return true;
        }
        return false;
    }

}
