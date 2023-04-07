using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsciAnimControl : MonoBehaviour
{
    [SerializeField] Animator anim; // iscpectorden atayýn!!
    [SerializeField] IsciAnimState animState;

    // IsciControl scriptinde asaðýdaki metotu kullanarak isciyi kontrol et.
    private void OnEnable()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }
    public void PlayClip(IsciAnimState _animState, float _speed)
    {
        if (animState == _animState) return;

        animState = _animState;
        bool _walking = false;
        int _walkIntRandom = 0;
        switch (_animState)
        {
            case IsciAnimState.idle:
                anim.Play("WK_worker_01_idle_A");
                break;
            case IsciAnimState.walkKazma:
                anim.Play("WK_worker_02_walk");
                break;
            case IsciAnimState.run:
                anim.Play("WK_worker_03_run");
                break;
            case IsciAnimState.attack:
                anim.Play("WK_worker_04_attack_A");
                break;
            case IsciAnimState.deathA:
                anim.Play("WK_worker_05_death_A");
                break;
            case IsciAnimState.deathB:
                anim.Play("WK_worker_06_death_B");
                break;
            case IsciAnimState.idleBag:
                anim.Play("WK_worker_carry_bag_01_idle");
                break;
            case IsciAnimState.walkBag:
                anim.Play("WK_worker_carry_bag_02_walk");
                break;
            case IsciAnimState.runBag:
                anim.Play("WK_worker_carry_bag_03_run");
                break;
            case IsciAnimState.attackBag:
                anim.Play("WK_worker_carry_bag_04_attack_A");
                break;
            case IsciAnimState.idleWood:
                anim.Play("WK_worker_carry_wood_01_idle");
                break;
            case IsciAnimState.walkWood:
                anim.Play("WK_worker_carry_wood_02_walk");
                break;
            case IsciAnimState.runWood:
                anim.Play("WK_worker_carry_wood_03_run");
                break;
            case IsciAnimState.attackWood:
                anim.Play("WK_worker_carry_wood_04_attack_A");
                break;
            case IsciAnimState.idleNoTool:
                anim.Play("workerIdle");
                break;
            case IsciAnimState.digPlant:
                anim.Play("digAndPlant");
                break;
            case IsciAnimState.sitting:
                anim.Play("idleSitting");
                break;
            case IsciAnimState.toSit:
                anim.Play("idleToSitup");
                break;
            case IsciAnimState.pickFruit:
                anim.Play("pickUpFruit_1");
                break;
            case IsciAnimState.fromSit:
                anim.Play("sitToidle");
                break;
            case IsciAnimState.wagonWalk:
                anim.Play("wagonTasiWalk");
                break;
            case IsciAnimState.walk:
                // anim.Play("workerWalk");
                _walkIntRandom = Random.Range(1, 3);
                _walking = true;
                anim.SetInteger("walkIntRandom", _walkIntRandom);
                anim.SetTrigger("walkTrigger");
                break;
        }
        anim.SetFloat("speed", _speed);
        anim.SetBool("walking", _walking);
        anim.SetInteger("walkIntRandom", _walkIntRandom);
    }

}
