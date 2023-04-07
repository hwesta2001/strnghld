using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class Unit
{
    [HideInInspector]
    public Transform trans, selectionArrowTransform; // unitmono da ayarlan�yor o y�zden HideInInspector!! // bu unit in Transform'u S�PER KULLANI�LI !!!
    //[HideInInspector]
    public NavMeshAgent agent; // unitmono da ayarlan�yor o y�zden HideInInspector!! 

    public string unitName;
    public Sprite icon;
    public UnitType unitType;
    [Space]
    public int team = 1; // 1inci Team her zaman player
    public bool isBuilding;
    [Space]
    public string info;
    [Space]
    public float maxHp;
    public float hp;
    public float armor = 1;
    [Space]
    public float attackSpeed = 2;
    public float attackRange = 10;
    public float aggroRange = 20; // agrroRange yar�captan ba��ms�z merkezden itibaren ekstra hesaplamaya gerek yok
    public float yariCap = 2; // unitin yar��ap� diyebiliriz
    [Space]
    public int minDamage = 1;
    public int maxDamage = 2;
    [Space]
    public Command command;
    public Transform target;





    [SerializeField]
    Vector3 prevDestination = new Vector3(999.757457f, 1923.5247f, 0); // ilk ba�lang�c di�erleri ile �ak��mas�n diye

    public void DamageUnit(int damage)
    {
        hp -= damage;
    }

    public void MoveToTarget(Vector3 destionation)
    {
        //Debug.Log("des : " + destionation + "prev : " + prevDestination);
        if (prevDestination == destionation) return;
        prevDestination = destionation;
        agent.SetAgentDestination(destionation);
        //Debug.LogError("unit Movign");
    }

    public void UnitStop()
    {
        agent.AgentStop();
        // unit idle animasyonu ba�lar...
    }

}

