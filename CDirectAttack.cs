
// bu command sadece bir target seciliyse çalýþýr.
// cManage.targetUnit seçimi ayrý bir metotla kontrol edilibilir.
// CommandManager de olabilir bu metot!!!
public class CDirectAttack : Commands
{


    void Update()
    {
        DireckAttack();
    }

    void DireckAttack()
    {
        if (cManager.targetUnit == null) return;
        if (InAttackRange())
        {
            AttakPhaseUpdate();
        }
        else
        {
            FollowTarget();
        }
    }

    void AttakPhaseUpdate()
    {
        cManager.attacking = true;

        cManager.unit.info = cManager.unit.unitName + " is attacking to " + cManager.targetUnit.unitName;
    }

    void FollowTarget()
    {
        cManager.attacking = false;
        cManager.unit.target = cManager.targetUnit.trans;
        cManager.unit.MoveToTarget(cManager.unit.target.position);
        cManager.unit.info = cManager.unit.unitName + " is chasing " + cManager.targetUnit.unitName;
    }

}
