
using System.Collections;
using UnityEngine;

public class CStandAndAttack : Commands
{
    Vector3 initPosition;
    bool movingBack;

    public override void OnEnable()
    {
        base.OnEnable();
        initPosition = transform.position;
        movingBack = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {

        if (cManager.unit.GetNearestInRange(out cManager.targetUnit, RangeType.Attack))
        {
            if (cManager.targetUnit == null) return;
            cManager.attacking = true;
            cManager.unit.info = cManager.unit.unitName + " is attacking to " + cManager.targetUnit.unitName;
            movingBack = false;
            AgentUpdateRot();
        }
        else
        {
            if (cManager.unit.GetNearestInRange(out cManager.targetUnit, RangeType.Aggro))
            {
                if (cManager.targetUnit == null) return;
                cManager.attacking = false;
                cManager.unit.info = cManager.unit.unitName + " is chasing to " + cManager.targetUnit.unitName;
                movingBack = false;
                cManager.unit.MoveToTarget(cManager.targetUnit.trans.position);
                LookToTarget();
            }
            else
            {
                cManager.attacking = false;
                cManager.targetUnit = null;
                cManager.unit.info = cManager.unit.unitName + " is ready to fight. Waiting enemies!";
                if (movingBack) return;
                movingBack = true;
                cManager.unit.MoveToTarget(initPosition);
                AgentUpdateRot();
            }

        }

    }
}
