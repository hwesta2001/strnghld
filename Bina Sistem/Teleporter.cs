using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    List<UnitType> triggeredUnit = new List<UnitType>();
    [SerializeField, Tooltip("boþ bir gameObject ile nereye teleport edileceði ayarlanacak")]
    Transform teleportTarget;

    private void OnEnable()
    {
        if (teleportTarget == null)
        {
            Debug.Log(gameObject.name + " teleportunda teleportTarget yok");
        }
        if (GetComponent<Collider>() == null)
        {
            Debug.Log(gameObject.name + " teleportunda collider yok");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UnitMono unitMono))
        {
            Unit unit = unitMono.unit;
            if (unit == null) return;
            if (!triggeredUnit.Contains(unit.unitType)) return;
            if (unit.unitType == UnitType.Player)
            {
                Globals.ins.PlayerTeleport(.2f);
            }
            other.transform.position = teleportTarget.position;
        }
    }
}
