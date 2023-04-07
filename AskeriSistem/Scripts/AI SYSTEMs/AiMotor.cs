using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMotor : MonoBehaviour
{

    [SerializeField]
    List<AiBehaviour> aiBehaviours = new List<AiBehaviour>();
    [SerializeField]
    List<Transform> spawnPoints = new List<Transform>();

    AiPrefabs aiPrefabs;


    private void Start()
    {
        aiPrefabs = GetComponent<AiPrefabs>();
        Invoke(nameof(InitAllAiBehaviour), 4f);
    }

    private void InitAllAiBehaviour()
    {
        foreach (var item in aiBehaviours)
        {
            StartCoroutine(InitRun(item, item.GlobalStartTime));
        }
    }



    IEnumerator InitRun(AiBehaviour aiBehaviour, float _time)
    {
        yield return new WaitForSeconds(_time);
        RunThisAi(aiBehaviour);
    }



    public void RunThisAi(AiBehaviour aiBehaviour)
    {

        foreach (var item in aiBehaviour.aiSteps)
        {
            StartCoroutine(RunThis(item));
        }

    }

    IEnumerator RunThis(AiSteps aiSteps)
    {
        yield return new WaitForSeconds(aiSteps.whenSpawn);
        for (int i = 0; i < aiSteps.count; i++)
        {
            Vector3 pot = spawnPoints[aiSteps.whereSpawn].position + Vector3.left * 2 * i;
            GameObject go = Instantiate(aiPrefabs.UnitPrefab(aiSteps.unitType), pot, Quaternion.identity);
            go.GetComponent<CommandManager>().InitCommand = aiSteps.initCommand;
        }

    }

}
