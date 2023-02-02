

public class CStop : Commands
{
    public override void OnEnable()
    {
        base.OnEnable();
        canTic = true;
    }
    void Update()
    {
        if (!canTic) return;
        StopNow();
    }
    void StopNow()
    {
        canTic = false;
        cManager.attacking = false;
        cManager.unit.UnitStop();
        cManager.unit.info = cManager.unit.unitName + " is waiting for command.";
    }
}
