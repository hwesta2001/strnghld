using System;
using UnityEngine;

// bir lastDestionationa ulaþýlacak nihayetinde.
// bu destination bir þekilde Ai tarafýndan ya da player tarafýndan belirlenir.

public class CAttackMove : Commands
{
    public Vector3 moveDestination = Vector3.zero;
    [SerializeField] //debug icin
    AttackPhase aPhase;
    bool movingTo;

    public override void OnEnable()
    {
        base.OnEnable();
        //SwitchPhase(AttackPhase.MovingToDestionation);
        aPhase = AttackPhase.MovingToDestionation;
        movingTo = false;
    }
    void SwitchPhase(AttackPhase _aPhase)
    {
        aPhase = _aPhase;
        switch (aPhase)
        {
            case AttackPhase.MovingToDestionation:
                AgentUpdateRot();
                cManager.attacking = false;
                cManager.unit.info = cManager.unit.unitName + " is moving to destination";
                break;
            case AttackPhase.Attacking:
                AgentUpdateRot();
                movingTo = false;
                cManager.attacking = true;
                cManager.unit.info = cManager.unit.unitName + " is attacking to " + cManager.targetUnit.unitName;
                break;
            case AttackPhase.Chasing:

                movingTo = false;
                cManager.unit.info = cManager.unit.unitName + " is chasing " + cManager.targetUnit.unitName;
                cManager.attacking = false;
                break;
            case AttackPhase.StandAndAttack:
                AgentUpdateRot();
                movingTo = false;
                cManager.unit.UnitStop();
                cManager.unit.command = Command.StandAndAttack;
                break;
            default:
                break;
        }
    }

    void Update()
    {

        MoveToDest();
        Chase();
        Attacking();

    }

    bool isMoveDestReached()
    {
        Vector3 offset = moveDestination - transform.position;
        float sqrLen = offset.sqrMagnitude;
        return sqrLen < 2f;
    }

    void MoveToDest()
    {
        if (aPhase != AttackPhase.MovingToDestionation) return;

        if (cManager.unit.GetNearestInRange(out cManager.targetUnit, RangeType.Aggro))
        {
            SwitchPhase(AttackPhase.Chasing);
        }
        else
        {
            if (isMoveDestReached())
            {
                SwitchPhase(AttackPhase.StandAndAttack);
                return;
            }
            else
            {
                if (movingTo) return;
                //Debug.Log("moving???");
                cManager.unit.MoveToTarget(moveDestination);
                movingTo = true;

            }

        }
    }

    void Chase()
    {
        if (aPhase != AttackPhase.Chasing) return;
        if (cManager.unit.GetNearestInRange(out cManager.targetUnit, RangeType.Attack))
        {
            AgentUpdateRot();
            SwitchPhase(AttackPhase.Attacking);
        }
        else
        {
            if (cManager.unit.GetNearestInRange(out cManager.targetUnit, RangeType.Aggro))
            {
                cManager.unit.MoveToTarget(cManager.targetUnit.trans.position);
                LookToTarget(8f);
            }
            else
            {
                AgentUpdateRot();
                SwitchPhase(AttackPhase.MovingToDestionation);
            }
        }

    }


    void Attacking()
    {
        if (aPhase != AttackPhase.Attacking) return;
        if (!cManager.unit.EnemyInAttackRange())
        {
            SwitchPhase(AttackPhase.Chasing);
        }
    }

}
