using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class UnitMono : MonoBehaviour
{
    [SerializeField] UnitSO unitSo;
    public Unit unit;
    float lastHp;
    public Transform selectLocation;

    void OnEnable()
    {
        if (unitSo != null)
        {
            unit = Instantiate(unitSo).unitTemplete;
        }
        else
        {
            Debug.Log(transform.name + " 'in UnitSO si eksik, UnitMono'daki Unit in verilerini kontrol edin");
        }

        unit.trans = transform;
        unit.agent = GetComponent<NavMeshAgent>();
        unit.selectionArrowTransform = selectLocation;
        lastHp = unit.hp;

        if (selectLocation == null)
        {
            Debug.Log(transform.name + " selectLocation transfomru eksik!");
        }

        Pools.AddToPool(unit, Pools.AllUnits);
    }

    void Update()
    {
        if (lastHp > unit.hp)
        {
            lastHp = unit.hp;
            DamageAnim();
        }
        if (lastHp < unit.hp)
        {
            lastHp = unit.hp;
            HealAnim();
        }
    }

    void HealAnim()
    {
        Debug.Log("HealAnim for " + unit.unitName);
    }

    void DamageAnim()
    {
        Debug.Log("DamageAnim for " + unit.unitName);
    }

    void LateUpdate()
    {
        if (unit.hp <= 0)
        {
            if (unit.unitType == UnitType.Player)
            {
                Debug.LogWarning("Player is dead! GG!!!");
            }
            else
            {
                DieUnit();
            }

        }
        if (unit.hp > unit.maxHp)
        {
            unit.hp = unit.maxHp;
        }
    }

    void DieUnit()
    {
        if (CanGlobals.ins.SelectedUnit == unit)
        {
            CanGlobals.ins.ClearSelection();
        }
        Pools.RemoveFromPool(unit, Pools.AllUnits);
        Destroy(gameObject, .01f);
    }

}
