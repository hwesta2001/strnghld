using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ISelection), typeof(UnitMono))]
public class PlayerAttack : MonoBehaviour
{
    //Unit myUnit;
    Unit targetUnit;
    ISelection selection;
    UnitMono unitMono;
    float ticTimer;
    public bool attacking; // atack animasyonlarý için kullanabiliriz.

    void OnEnable()
    {
        selection = GetComponent<ISelection>();
        unitMono = GetComponent<UnitMono>();
        ticTimer = unitMono.unit.attackSpeed;
    }

    void Update()
    {
        AttackControl();
        selection.AttackBool(attacking);
    }

    void AttackControl()
    {
        if (!CanAttack())
        {
            attacking = false;
            return;
        }
        attacking = true;
        AttakTick();
    }

    bool CanAttack()
    {
        targetUnit = selection.GetUnit();
        if (targetUnit == null)
        {
            return false;
        }
        else
        {
            if (unitMono.unit.team != targetUnit.team)
            {
                if (RangeOk())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    bool RangeOk()
    {
        return selection.GetRange();
    }

    void AttakTick()
    {
        ticTimer -= Time.deltaTime;
        if (ticTimer <= 0)
        {
            StartCoroutine(DamageToSelection());
            ticTimer = unitMono.unit.attackSpeed;
        }
    }

    IEnumerator DamageToSelection()
    {

        float damageReduction = (targetUnit.armor * CanGlobals.ins.globalArmorConstant)
                                / (1 + (targetUnit.armor * CanGlobals.ins.globalArmorConstant));
        int rawDamage = UnityEngine.Random.Range(unitMono.unit.minDamage, unitMono.unit.maxDamage);
        int damage = Mathf.FloorToInt(rawDamage - (rawDamage * damageReduction));
        damage = Mathf.Clamp(damage, 1, 9999999);
        //Debug.Log("damageReduction= " + damageReduction);
        //Debug.Log("rawDamage= " + rawDamage);
        //Debug.Log("damage= " + damage);
        PlayAttackAnim(damage);
        yield return new WaitForSeconds(.4f);
        if (targetUnit == null) yield break;
        targetUnit.DamageUnit(damage);
    }

    void PlayAttackAnim(int damage)
    {
        if (!attacking) return;
        selection.AttackAnimTic(damage);
    }
}
