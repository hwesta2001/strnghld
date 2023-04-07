
using UnityEngine;

public class CMove : Commands
{
    public Vector3 lastDestination;

    public override void OnEnable()
    {
        base.OnEnable();
        MoveNow();
    }
    void Update()
    {
        if (isMoveDestReached())
        {
            ChangeToCommand(Command.StandAndAttack);
        }

        MoveOneTime();

    }

    void MoveNow()
    {
        canTic = true;
    }

    void MoveOneTime()
    {
        if (!canTic) return;
        canTic = false;
        cManager.unit.MoveToTarget(lastDestination);
        cManager.unit.info = cManager.unit.unitName + " is moving to destination.";
    }



    bool isMoveDestReached()
    {
        Vector3 offset = lastDestination - transform.position;
        float sqrLen = offset.sqrMagnitude;
        return sqrLen < 2f;
    }
}
