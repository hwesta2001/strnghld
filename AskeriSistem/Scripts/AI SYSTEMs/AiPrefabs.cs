using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPrefabs : MonoBehaviour
{

    public GameObject pfARCHER;
    public GameObject pfPIKEMAN;
    public GameObject pfSWORDSMAN;
    // DÝÐER TÜM ASKER VB UNÝTLERÝN PREFABLARINI BURAYA EKLE!!!

    public GameObject UnitPrefab(UnitType unitType)
    {
        GameObject go = null;
        switch (unitType)
        {
            case UnitType.Archer:
                go = pfARCHER;
                break;
            case UnitType.Bina:
                break;
            case UnitType.Swordsman:
                go = pfSWORDSMAN;
                break;
            case UnitType.Pikeman:
                go = pfPIKEMAN;
                break;
            case UnitType.Player:
                break;
            default:
                break;
        }
        return go;
    }


}