using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneQuaryWork : Works
{

    enum States { isYerine, IseYerles, tasCikar }
    [SerializeField] States state;


    [SerializeField] Transform tr;
    LocalResourceSayac localStoneSayac;

    void OnEnable()
    {
        isciControl = GetComponent<IsciControl>();
        binaGorevi = IsciGorevi.TasUstasi;
        baslangicTik = false;
    }

    void Update()
    {
        IsyeriAktifKontrol();
        BaslaTic();
    }

    void BaslaTic()
    {
        if (baslangicTik) return;
        baslangicTik = true;
        StateControl(States.isYerine);
        SetPoints(isyeriKapi.parent.GetComponent<IsYerindekiler>());
        localStoneSayac = isyeriKapi.parent.GetComponent<LocalResourceSayac>();
    }

    void SetPoints(IsYerindekiler iy)
    {
        tr = iy.isPointleri[0];
        if (iy.isPointleri.Count > 1)
        {
            iy.isPointleri.Remove(tr);
        }
    }


    void StateControl(States _state)
    {
        state = _state;
        switch (state)
        {
            case States.isYerine:
                IsyerineGit();
                break;

            case States.IseYerles:
                IseYerles();
                break;

            case States.tasCikar:
                TasCikar();
                break;
        }
    }

    void IseYerles()
    {
        StopAgent();
        
        AgentGoToAndDO(tr.position, IsciAnimState.walkKazma, 0.6f, () => StateControl(States.tasCikar));
    }

    void TasCikar()
    {
        StopAgent();
        StartCoroutine(ResetIs());
    }

    IEnumerator ResetIs()
    {
        SetAnimation(IsciAnimState.attack);
        yield return new WaitForSeconds(6);
        SetAnimation(IsciAnimState.pickFruit);
        yield return new WaitForSeconds(3.5f);
        SetAnimation(IsciAnimState.idleBag);
        yield return new WaitForSeconds(2);
        SetAnimation(IsciAnimState.attackBag);
        yield return new WaitForSeconds(3);
        int i = UnityEngine.Random.Range(3, 5);
        localStoneSayac.SetLocalStone(i);
        TasCikar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == isyeriKapi)
        {
            if (state == States.isYerine)
            {
                StateControl(States.IseYerles);
            }
        }
    }
}