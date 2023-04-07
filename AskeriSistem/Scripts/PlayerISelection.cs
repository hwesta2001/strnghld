using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerISelection : MonoBehaviour, ISelection
{
    [SerializeField] Camera cam; // cachele bunu globalsden falan..
    Unit returnUnit;
    Unit myUnit;
    bool attacking;
    Color col = Color.green;

    bool facingWrong;

    void OnEnable()
    {
        myUnit = GetComponent<UnitMono>().unit;
    }

    void Update()
    {
        SelectionControl();

        SelectionArrowColor();
        if (facingWrong)
        {
            if (TextControl.ins.writtingNow) return;
            TextControl.ins.AlertText("Facing wrong direction!", 28, Color.red, 8f);
        }

    }

    void LateUpdate()
    {
        if (CanGlobals.ins.SelectedUnit == CanGlobals.ins.emptyUnit)
        {
            facingWrong = false;
            returnUnit = null;
        }

    }

    void SelectionArrowColor()
    {
        if (returnUnit == null) return;
        if (attacking)
        {
            col = Color.red;

        }
        else
        {

            if (returnUnit.team != 1)  // 1inci Team her zaman player bu satýr enemy demek
            {

                col = Color.yellow;
            }
            else
            {

                col = Color.green;
            }
        }
        CanGlobals.ins.SelectionArrowColor(col);
    }

    public void SelectionControl()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo); // layermask ekleyebiliriz.
            if (hit)
            {
                if (hitInfo.transform.GetComponent<UnitMono>() != null)
                {
                    UnitMono um = hitInfo.transform.GetComponent<UnitMono>(); //.SelectThis();
                    if (um.unit.unitType != UnitType.Player)
                    {
                        returnUnit = um.unit;
                        SelectThis(returnUnit, returnUnit.selectionArrowTransform.position);
                    }
                }
                else
                {
                    CanGlobals.ins.ClearSelection();
                    returnUnit = null;
                }
            }
            else
            {
                CanGlobals.ins.ClearSelection();
                returnUnit = null;
            }
        }
    }



    public Unit GetUnit()
    {
        return returnUnit;
    }

    public bool GetRange()
    {
        if (returnUnit == null) return false;
        if (returnUnit.trans == null) return false;

        if (Vector3.Distance(transform.position, returnUnit.trans.position) <= (myUnit.attackRange + returnUnit.yariCap))
        {
            Vector3 targetDir = returnUnit.trans.position - transform.position;
            float angle = Vector3.Angle(targetDir, transform.forward);

            if (angle < 20.0f)
            {
                facingWrong = false;
                return true;
            }
            else
            {
                //TextS.ins.AlertText("Facing wrong direction!", 36, Color.red, .009f);
                facingWrong = true;
                return false;
            }
        }
        else
        {
            facingWrong = false;
            return false;
        }

        //return Vector3.Distance(transform.position, returnUnit.trans.position) <= myUnit.attackRange;
    }


    void SelectThis(Unit _unit, Vector3 pos)
    {
        CanGlobals.ins.SelectedUnit = _unit;

        CanGlobals.ins.SelectionArrowMove(pos, _unit.yariCap);
        facingWrong = false;
    }

    public void AttackBool(bool con)
    {
        attacking = con;
    }

    public void AttackAnimTic(int damage)
    {
        HandAnimGlobal.ins.AttackTic(true);
        //CanGlobals.ins.FloatingText(damage.ToString(), Color.yellow);
    }
}
