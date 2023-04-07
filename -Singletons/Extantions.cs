using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public static class Extantions
{

    // DoRotateY baslangýç

    public delegate void Func();
    public static void DORotateY(this Transform trans, float? time = null, int? loop = null, Ease? ease = null, Func func = null)
    {
        Func _f = func is null ? FisEmpty : func;

        int a = loop * 6 ?? 6;
        if (loop <= 0) a = -1;

        var _time = time / a ?? 1f;
        if (a <= -1) _time = 6;


        trans.DOLocalRotate(Vector3.up * 60 + trans.localRotation.eulerAngles, _time)
             .SetLoops(a, LoopType.Incremental)
             .SetEase(ease ?? Ease.Linear)
             .OnComplete(() => _f());
    }

    public static void FisEmpty()
    {
        Debug.Log("Function is null");
    }

    // DoRotateY bitiþ----------------------------------

    public static void DOYuksel(this Transform tr, float yukseklik, float? time = null, Ease? ease = null)
    {
        Vector3 point = tr.position;
        tr.position = new Vector3(point.x, point.y - yukseklik, point.z);
        tr.DOMove(point, time ?? 4).SetEase(ease ?? Ease.Linear);
    }

    public static void DoFloat(float _float, float targetValue, float? time = null)
    {
        DOTween.To(x => _float = x, _float, targetValue, time ?? 1f);
    }

    public static Color ColorTick(Color? color1 = null, Color? color2 = null, float? time = null)
    {
        return Color.Lerp(color1 ?? Color.white, color2 ?? Color.red, Mathf.PingPong(Time.unscaledTime, time ?? 0.8f));

    }

    public static GameObject BinaKur(GameObject prefab, Transform insaat, float? time = null)
    {

        return Globals.ins.BinaKur(prefab, insaat, time ?? 4f); // instantiate monobehaviourdaymýþ bu yüzden globals.ins ten caðýrýyoruz.
    }


    public static Vector3 Onunde(Transform target, float? ara = null) // ara kadar targetin önüne yere yerleþtirir.
    {
        float a = ara ?? 12f;
        Vector3 forwardPoint = target.position + target.forward * a;
        forwardPoint.y = 0;
        return forwardPoint;
    }

    public static void LookAtY(this Transform trans, Transform target, float? damping = null)
    {
        if (trans == null || target == null) return;
        //Vector3 targetPostition = new Vector3(target.position.x, trans.position.y, target.position.z);
        //trans.LookAt(targetPostition);

        var lookPos = target.position - trans.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        trans.rotation = Quaternion.Slerp(trans.rotation, rotation, Time.deltaTime * damping ?? 10f);
    }

    static void LookAtYPosition(Transform tr, Vector3 target)
    {
        var lookPos = target - tr.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        tr.rotation = rotation;// Quaternion.Slerp(tr.rotation, rotation, 10f);
    }

    public static void DOGoToPoints5(this Transform trans, Vector3[] targetPos, float time)
    {
        Sequence mySequence = DOTween.Sequence();
        int a = 0;
        for (int i = 0; i < targetPos.Length; i++)
        {
            mySequence.Append(trans.DOMove(targetPos[i], time).OnComplete(() => a++));

        }
        a = Mathf.Clamp(a, 0, targetPos.Length - 2);
        mySequence.Play().OnUpdate(() => LookAtYPosition(trans, targetPos[a]));
        //trans.DOMove(targetPos[0], time).OnComplete(() => trans.DOMove(targetPos[1], time)
        //                                .OnComplete(() => trans.DOMove(targetPos[2], time)
        //                                .OnComplete(() => trans.DOMove(targetPos[3], time)
        //                                .OnComplete(() => trans.DOMove(targetPos[4], time)))));

    }

    public static Vector3 NesneyeBak90(Transform obje)  // Transformun objeye göre ne tarafta olduðunu bulur ve 90derece acýlarla objeye döner
    {
        Vector3 tranformYonu = Vector3.zero;
        float x = obje.forward.x;
        float z = obje.forward.z;

        if (Mathf.Abs(x) <= Mathf.Abs(z))
        {
            if (z >= 0)
            {
                tranformYonu = Vector3.up * 180;
            }
            else
            {
                tranformYonu = Vector3.zero;
            }
        }
        else
        {
            if (x >= 0)
            {
                tranformYonu = Vector3.up * -90;
            }
            else
            {
                tranformYonu = Vector3.up * 90;
            }
        }
        return tranformYonu;
    }

    public static void SetAgentDestination(this NavMeshAgent agent, Vector3 point)
    {
        agent.isStopped = false;
        agent.ResetPath();
        agent.SetDestination(point);
    }

    public static void AgentStop(this NavMeshAgent agent)
    {
        if (agent.isStopped == true) return;
        agent.isStopped = true;
        //agent.ResetPath();
    }

    public static bool AgentHasNoPath(this NavMeshAgent agent, Vector3 tr)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(tr, path);
        if (path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial)
        {
            return true;
        }
        return false;
    }



    public static Transform FindAChild(this Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName && child.gameObject.activeInHierarchy)
            {
                return child;
            }
            else
            {
                Transform found = FindAChild(child, childName);
                if (found != null)
                {
                    return found;
                }
            }
        }
        return null;
    }








}
