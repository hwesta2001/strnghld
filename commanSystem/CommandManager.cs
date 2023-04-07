using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMono))]
public class CommandManager : MonoBehaviour
{
    List<Commands> commands = new List<Commands>(); // disableAll commands icin kullanýlýyor sadece.
    public Command InitCommand = Command.Stop;
    [SerializeField] // serializeFiled inspectorde commandý takip etmek icin.
    Command currentCommand;

    public Unit unit;
    public Unit targetUnit;
    public bool attacking;


    Unit lastTargetUnit;

    CMove cMove;
    CStop cStop;
    CAttackMove cAttackMove;
    CDirectAttack cDirectAttack;
    CStandAndAttack cStandAndAttack;
    CSearchAndGoToAttack cSearch;

    bool initCahced;
    void OnEnable()
    {
        initCahced = false;
        StartCoroutine(ChachStart()); // chachi biraz gec baslatýyoruz ki diðer awakeler iyice bitsin.
        currentCommand = Command.Stop;
    }

    void ChacheAll()
    {
        unit = GetComponent<UnitMono>().unit;
        cMove = GetComponent<CMove>();
        cStop = GetComponent<CStop>();
        cAttackMove = GetComponent<CAttackMove>();
        cDirectAttack = GetComponent<CDirectAttack>();
        cStandAndAttack = GetComponent<CStandAndAttack>();
        cSearch = GetComponent<CSearchAndGoToAttack>();
        lastTargetUnit = null;
        unit.command = InitCommand;
    }


    IEnumerator ChachStart()
    {
        //yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitForSeconds(1f);

        ChacheAll();

        foreach (Commands item in GetComponents<Commands>())
        {
            commands.Add(item);
        }
        DisableAllCommands();
        initCahced = true;
        yield return null;
    }

    void Update()
    {
        if (!initCahced) return;
        SwitchCommandControl();
        TargetUnitcontrol();
    }

    void SwitchCommandControl()
    {
        if (currentCommand == unit.command) return;
        currentCommand = unit.command;
        SwitchCommand(unit.command);
    }

    void TargetUnitcontrol()
    {
        if (lastTargetUnit == targetUnit) return;
        lastTargetUnit = targetUnit;
        if (targetUnit == null) return;
        unit.target = targetUnit.trans;
    }

    void SwitchCommand(Command com)
    {
        DisableAllCommands();


        switch (com)
        {
            case Command.Move:
                if (cMove == null) return;
                Debug.Log("command control12" + com);
                cMove.enabled = true;
                break;
            case Command.Stop:
                if (cStop == null) return;
                cStop.enabled = true;
                break;
            case Command.AttackMove:
                if (cAttackMove == null) return;
                cAttackMove.enabled = true;
                break;
            case Command.DirectAttack:
                if (cDirectAttack == null) return;
                cDirectAttack.enabled = true;
                break;
            case Command.StandAndAttack:
                if (cStandAndAttack == null) return;
                cStandAndAttack.enabled = true;
                break;
            case Command.SearchAndGoToAttack:
                if (cSearch == null) return;
                cSearch.enabled = true;
                break;
            default:
                break;
        }
    }

    void DisableAllCommands()
    {
        //if (commadans.Count <= 0) return;
        foreach (Commands item in commands)
        {
            item.enabled = false;
        }
    }

    public void SetTargetUnit(Unit _targetUnit)
    {
        targetUnit = _targetUnit;
    }
}
