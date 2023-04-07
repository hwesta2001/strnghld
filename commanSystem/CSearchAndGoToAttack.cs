using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSearchAndGoToAttack : Commands
{
    [SerializeField] float minRange = 5f, maxRange = 10f;
    Vector3 targetPos, initPos;
    bool tic;
    int count;

    public override void OnEnable()
    {
        base.OnEnable();
        targetPos = transform.position;
        initPos = transform.position;
        tic = true;
        count = 0;
    }


    private void Update()
    {
        if (!tic) return;
        if (TargetToTransformDistanceControl())
        {
            StartCoroutine(GoToStart(4f));
        }


    }

    IEnumerator GoToStart(float delay)
    {
        tic = false;
        cManager.unit.UnitStop();
        yield return new WaitForSeconds(delay);

        if (count >= 2)
        {
            targetPos = initPos;
            count = 0;
        }
        else
        {
            targetPos = GenerateRandomWanderPoint();
        }
        cManager.unit.MoveToTarget(targetPos);
        tic = true;
        yield return null;

    }

    bool TargetToTransformDistanceControl()
    {
        Vector3 offset = targetPos - transform.position;
        float sqrLen = offset.sqrMagnitude;
        return sqrLen < 2f;
    }







    Vector3 GenerateRandomWanderPoint()
    {
        Vector3 pos = transform.position;

        float x1 = Random.Range(-maxRange, -minRange);
        float x2 = Random.Range(minRange, maxRange);
        float z1 = Random.Range(-maxRange, -minRange);
        float z2 = Random.Range(minRange, maxRange);

        if (Random.value <= 0.5f)
        {
            if (Random.value <= 0.5f)
            {
                pos = new Vector3(pos.x + x1, pos.y, pos.z + z1);
            }
            else
            {
                pos = new Vector3(pos.x + x1, pos.y, pos.z + z2);
            }
        }
        else
        {
            if (Random.value <= 0.5f)
            {
                pos = new Vector3(pos.x + x2, pos.y, pos.z + z1);
            }
            else
            {
                pos = new Vector3(pos.x + x2, pos.y, pos.z + z2);
            }
        }
        count++;
        return pos;
    }
}
