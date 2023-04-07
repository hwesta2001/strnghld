using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using DG.Tweening;

public class AgacWood : MonoBehaviour
{

    public int agactakiWood;
    NavMeshObstacle obstacle;
    void OnEnable()
    {

        Pools.AddToPool(transform, Pools.agaclar);
    }

    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        Pools.AddToPool(transform, Pools.agaclar);
        Debug.Log(" agac sayýsý" + Pools.agaclar.Count);
    }

    public void AgacWoodSet(int birimWoodEksik)
    {
        agactakiWood -= birimWoodEksik;
        if (agactakiWood <= 0)
        {
            AgacYikilmasi();
        }
    }

    public void AgacYikilmasi()
    {
        Pools.RemoveFromPool(transform, Pools.agaclar);
        StartCoroutine(AfilliAgacYikilmasiFirst());
        // afilli agac yýkýlma animasyonu ekle vs.
    }

    IEnumerator AfilliAgacYikilmasiFirst()
    {
        int i = 1;
        if (Random.value < 0.5f)
        {
            i *= -1;
        }
        yield return new WaitForSeconds(.4f);
        float x = Random.Range(4, 30);
        x *= i;
        transform.DOLocalRotate(new Vector3(x, 0, 0), 1f)
            .SetEase(Ease.InBounce).OnComplete(() => AfilliAgacYikilmasiSecond(i));

    }

    void AfilliAgacYikilmasiSecond(int i)
    {
        float x = Random.Range(-30, -40);
        x *= i;
        transform.DOLocalRotate(new Vector3(x, 0, 0), 1.4f)
            .SetEase(Ease.InOutBounce).OnComplete(() => AfilliAgacYikilmasiLast(i));
    }
    void AfilliAgacYikilmasiLast(int i)
    {
        obstacle.enabled = false;
        transform.DOLocalRotate(new Vector3(90 * i, 0, 0), 2.8f)
            .SetEase(Ease.OutBounce).OnComplete(DestroyAgac);
    }

    void DestroyAgac()
    {
        Destroy(gameObject, .2f);
    }

    void OnDisable()
    {
        Pools.RemoveFromPool(transform, Pools.agaclar);
    }
}
