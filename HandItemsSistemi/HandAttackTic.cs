using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttackTic : MonoBehaviour
{
    public void AttackTic()
    {
        HandAnimGlobal.ins.AttackTic(false);
    }

}
