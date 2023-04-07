
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class IsciControl : MonoBehaviour
{
    // DEbug icin
    Transform DebugTop;
    private void Update()
    {
        if (!DebugTop) return;
        DebugTop.position = agent.destination;
    }
    // DEbug icin

    public Isci isci;
    [SerializeField] string[] isciIsimleri;
    public Transform _isyeri;
    public NavMeshAgent agent;
    [SerializeField]
    IsciAnimControl isciAnimController;

    //IsciAnimControl ia;
    void OnEnable()
    {


        StartCoroutine(InitDelay());
    }

    IEnumerator InitDelay()
    {
        yield return new WaitForSeconds(.5f);
        DebugTop = Instantiate(Globals.ins.DebugTop, Globals.ins.transform); //debug icin
        int i = Random.Range(0, isciIsimleri.Length);
        isci.name = isciIsimleri[i];

        /*isciAnimController = GetComponent<IsciAnimControl>();*/ // inspectorden belki???

        //ia = GetComponent<IsciAnimControl>();
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(60, 100);
        this.AddToPool(Pools.Isciler);
        IsciGorevSet(IsciGorevi.Idle);
    }

    void OnDisable()
    {

        IsciGorevSet(IsciGorevi.Idle);
        Globals.ins.SetGlobals(GloabalResource.Population, -1);
        this.RemoveFromPool(Pools.Isciler);
    }

    public void IsciGorevSet(IsciGorevi g, Transform tr = null)
    {
        isci.gorev = g;
        WorksReset();
        Transform trans = tr != null ? tr : _isyeri;
        // oduncu stonecu ıroncı scriptlerinin birini state göre ekle
        switch (g)
        {
            case IsciGorevi.Idle:
                IdleWork idleWork = gameObject.AddComponent<IdleWork>();
                idleWork.isciControl = this;
                break;
            case IsciGorevi.Oduncu:
                OduncuWork oduncu = gameObject.AddComponent<OduncuWork>();
                _isyeri = trans;
                oduncu.IsyeriKapi(trans);
                break;
            case IsciGorevi.TasUstasi:
                _isyeri = trans;
                gameObject.AddComponent<StoneQuaryWork>().IsyeriKapi(trans);
                break;
            case IsciGorevi.Wagoncu:
                _isyeri = trans;
                gameObject.AddComponent<WagoncuWork>().IsyeriKapi(trans);
                break;
            case IsciGorevi.Madenci:
                break;
            case IsciGorevi.Asker:
                Debug.LogError("asker miii");
                break;
            case IsciGorevi.NonWorker:
                break;
            case IsciGorevi.WheatFarmer:
                _isyeri = trans;
                gameObject.AddComponent<FarmWheatWork>().IsyeriKapi(trans);
                break;
            case IsciGorevi.AppleFarmer:
                _isyeri = trans;
                gameObject.AddComponent<AppleFarmWork>().IsyeriKapi(trans);
                break;
            case IsciGorevi.MillWorker:
                _isyeri = trans;
                gameObject.AddComponent<MillWork>().IsyeriKapi(trans);
                break;
            case IsciGorevi.Bakery:
                _isyeri = trans;
                gameObject.AddComponent<BakeryWork>().IsyeriKapi(trans);
                break;
            default:
                break;
        }
    }

    void WorksReset()
    {
        Works[] w = GetComponents<Works>();
        if (w == null || w.Length <= 0) return;
        for (int i = 0; i < w.Length; i++)
        {
            Destroy(w[i]);
        }
    }

    public void AnimSet(IsciAnimState animState, float _speed)
    {
        //ia.TriggerThis((int)animState);

        float speed = agent.speed * .49f * _speed;
        if (animState == IsciAnimState.attack || animState == IsciAnimState.attackBag || animState == IsciAnimState.attackWood
            || animState == IsciAnimState.idle || animState == IsciAnimState.idleBag || animState == IsciAnimState.idleWood)
        {
            speed = _speed;
        }

        isciAnimController.PlayClip(animState, speed);
    }

    public void AgentToDest(Vector3 point)
    {
        //agent.isStopped = false;
        //agent.ResetPath();
        agent.SetAgentDestination(point);
        //ia.SetMoveSpeed(agent.speed * .67f);
    }

    public void AgentStop()
    {
        agent.AgentStop();
    }

    public bool AgentHasNoPath(Vector3 tr)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(tr, path);
        if (path.status == NavMeshPathStatus.PathInvalid || path.status == NavMeshPathStatus.PathPartial)
        {
            return true;
        }
        return false;


        //return agent.AgentHasNoPath(tr);
    }


    public Vector3 RandomNavmeshLocation(Vector3 center, float radius)
    {

        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

}