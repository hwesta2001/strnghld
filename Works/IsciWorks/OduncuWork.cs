using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OduncuWork : Works
{
    enum States { Oduncuya, Oduncuda, Agaca, Agacta, depoya, depoda }

    [SerializeField] States state;
    bool tik;
    bool agactan;
    bool controlAgac;

    AgacWood agacWood;

    Transform agac;
    Transform depoKapi;

    void OnEnable()
    {
        isciControl = GetComponent<IsciControl>();
        agactan = false;
        binaGorevi = IsciGorevi.Oduncu;
        StateControl(States.Oduncuya);
        tik = false;
    }


    void Update()
    {
        IsyeriAktifKontrol();
        ControlAgac();
        if (tik) return;
        StateControl(state);
    }



    void StateControl(States _state)
    {
        StopAllCoroutines();
        tik = true;
        state = _state;
        switch (state)
        {
            case States.Oduncuya:
                Oduncuya();
                break;
            case States.Oduncuda:
                Oduncuda();
                break;
            case States.Agaca:
                Agaca();
                break;
            case States.Agacta:
                Agacta();
                break;
            case States.depoya:
                Depoya();
                break;
            case States.depoda:
                Depoda();
                break;
            default:
                break;
        }
    }

    void Depoda()
    {

        SetAnimation(IsciAnimState.attackWood);
        StopAgent();
        StartCoroutine(DepoIsle());
    }

    IEnumerator DepoIsle()
    {
        yield return new WaitForSeconds(3);
        Globals.ins.SetGlobals(GloabalResource.Wood, Globals.ins.woodDegisimMiktari);
        StateControl(States.Agaca);
    }

    void Depoya()
    {
        depoKapi = Pools.EnYakinDepoBul(isyeriKapi, Pools.depolar);
        SetAgentDestination(depoKapi.position);
        SetAnimation(IsciAnimState.walkWood);
    }

    void Agacta()
    {
        SetAnimation(IsciAnimState.attack);
        StopAgent();
        StartCoroutine(AgacIsle());
    }

    IEnumerator AgacIsle()
    {
        yield return new WaitForSeconds(5);
        agactan = true;
        if (agacWood != null)
        {
            agacWood.AgacWoodSet(1);
        }
        StateControl(States.Oduncuya);
        controlAgac = false;
    }

    void Agaca()
    {
        controlAgac = true;
        if (Pools.agaclar.Count <= 0)
        {
            TextControl.ins.AlertText(" Agac Yok!", color: Color.red, gecikme: .01f);
            return;
        }
        agac = Pools.EnYakinBul(isyeriKapi, Pools.agaclar);

        if (agac != null)
        {
            agacWood = agac.GetComponent<AgacWood>();
        }
        SetAgentDestination(agac.position);
        SetAnimation(IsciAnimState.walkKazma);
    }

    void ControlAgac()
    {
        if (controlAgac)
        {
            if (agac == null)
            {
                Debug.Log("Yýkýlan agac");
                StateControl(States.Agaca);
                return;
            }
            if (!agac.gameObject.activeSelf)
            {
                Debug.Log("Yeni agac");
                StateControl(States.Agaca);
                return;
            }
            if (agac != null && agacWood != null && agacWood.agactakiWood <= 0)
            {
                agacWood.AgacYikilmasi(); //bunu kontrol için ekledim buildde kalkacak
                Debug.Log("Agacta Odun Bitti");
                StateControl(States.Agaca);
                return;
            }
        }
    }
    void Oduncuda()
    {
        if (agactan)
        {
            SetAnimation(IsciAnimState.attackWood);
        }
        else
        {
            SetAnimation(IsciAnimState.attack);
        }
        StopAgent();
        StartCoroutine(OdunIsle());
    }

    IEnumerator OdunIsle()
    {
        transform.DOLookAt(isyeriKapi.position, 2);
        yield return new WaitForSeconds(4);
        if (agactan)
        {
            StateControl(States.depoya);
        }
        else
        {
            StateControl(States.Agaca);
        }
    }

    void Oduncuya()
    {
        if (isyeriKapi == null)
        {
            Debug.Log("oduncu bos");
            return;
        }
        tik = true;
        SetAgentDestination(isyeriKapi.position);
        if (agactan)
        {
            SetAnimation(IsciAnimState.walkWood);
        }
        else
        {
            SetAnimation(IsciAnimState.walkKazma);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state == States.Oduncuya)
            {
                StateControl(States.Oduncuda);
            }
        }
        if (other.transform == agac)
        {
            if (state == States.Agaca)
            {
                StateControl(States.Agacta);
            }
        }
        if (other.transform == depoKapi)
        {
            if (state == States.depoya)
            {
                StateControl(States.depoda);
            }
        }
    }
}
