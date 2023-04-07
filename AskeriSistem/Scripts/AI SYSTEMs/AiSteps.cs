using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AiSteps
{
    public UnitType unitType;
    [Tooltip("How Many Unity SpawnOlacak")]
    public int count;
    [Tooltip("Ne zaman SpawnOlacak")]
    public int whenSpawn;
    [Tooltip("EnemySpawnLocation Transformlarý nin list nosu 0,1,2 vb..")]
    public int whereSpawn = 0;
    public Command initCommand;
    public List<TimedCommand> timedCommands = new List<TimedCommand>();
}

public class TimedCommand
{
    public int when;
    public Command whatToDo;
}
