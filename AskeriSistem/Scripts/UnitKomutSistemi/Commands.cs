using UnityEngine;


public class Commands : MonoBehaviour
{
    public CommandManager cManager;
    public bool canTic;

    public virtual void OnEnable()
    {

        cManager = GetComponent<CommandManager>();
        Invoke(nameof(AgentUpdateRot), 0.2f);
    }

    protected void ChangeToCommand(Command com)
    {
        cManager.unit.command = com;
    }

    public bool InAttackRange()
    {
        return cManager.unit.InAttackRange(cManager.targetUnit);
    }

    public void LookToTarget(float? damp = null)
    {
        cManager.unit.trans.LookAtY(cManager.unit.target, damp ?? 4f);
        cManager.unit.agent.updateRotation = false;
    }
    public void AgentUpdateRot()
    {
        cManager.unit.agent.updateRotation = true;
    }


}
