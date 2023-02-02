using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitAttack : MonoBehaviour
{
    CommandManager cManager;
    float ticTimer;

    void OnEnable()
    {
        cManager = GetComponent<CommandManager>();
        ticTimer = cManager.unit.attackSpeed;
    }

    void Update()
    {
        if (!cManager.attacking) return;
        AttakTick();

        //}
        //void LateUpdate()
        //{
        //    if (!cManager.attacking) return;

        FaceToTarget();
    }
    void FaceToTarget()
    {
        cManager.unit.UnitStop();
        cManager.unit.trans.LookAtY(cManager.unit.target, 15f);
    }

    void AttakTick()
    {
        ticTimer -= Time.deltaTime;
        if (ticTimer <= 0)
        {
            StartCoroutine(DamageToSelection());
            ticTimer = cManager.unit.attackSpeed;
        }
    }



    IEnumerator DamageToSelection()
    {

        float damageReduction = (cManager.targetUnit.armor * CanGlobals.ins.globalArmorConstant)
                                / (1 + (cManager.targetUnit.armor * CanGlobals.ins.globalArmorConstant));

        int rawDamage = Random.Range(cManager.unit.minDamage, cManager.unit.maxDamage);
        int damage = Mathf.FloorToInt(rawDamage - (rawDamage * damageReduction));
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        yield return new WaitForSeconds(.001f);
        cManager.targetUnit.DamageUnit(damage);
    }
}
