using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RangeControl
{
    // Extantions a tasýyabiliriz alttaki metotlarý 
    public static bool InAttackRange(this Unit unit, Unit targetUnit)
    {
        if (targetUnit == null) return false;
        Vector3 offset = targetUnit.trans.position - unit.trans.position;
        return offset.sqrMagnitude <= unit.attackRange * unit.attackRange;
    }

    public static bool InAggroRange(this Unit unit, Unit targetUnit)
    {
        if (targetUnit == null) return false;
        Vector3 offset = targetUnit.trans.position - unit.trans.position;
        return offset.sqrMagnitude <= unit.aggroRange * unit.aggroRange;
    }



    public static bool EnemyInAttackRange(this Unit unit)
    {
        int a = 0;
        foreach (Unit item in Pools.AllUnits)
        {
            if (item.team != unit.team)
            {
                if (unit.InAttackRange(item))
                {
                    a++;
                }
            }
        }
        if (a > 0)
        {
            return true;
        }
        return false;
    }

    public static bool GetNearestInRange(this Unit unit, out Unit targetUnit, RangeType rangeType)
    {
        float dis1 = float.MaxValue;
        Unit returnUnit = null;
        int a = 0;
        foreach (Unit item in Pools.AllUnits)
        {
            if (item.team != unit.team)
            {
                if (rangeType == RangeType.Attack)
                {
                    if (unit.InAttackRange(item))
                    {
                        a++;
                        float dis = (item.trans.position - unit.trans.position).sqrMagnitude;
                        if (dis < dis1)
                        {
                            dis1 = dis;
                            returnUnit = item;
                        }

                    }
                }
                else if (rangeType == RangeType.Aggro)
                {
                    if (unit.InAggroRange(item))
                    {
                        a++;
                        float dis = (item.trans.position - unit.trans.position).sqrMagnitude;
                        if (dis < dis1)
                        {
                            dis1 = dis;
                            returnUnit = item;
                        }

                    }
                }

            }
        }
        targetUnit = returnUnit;
        if (a > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
