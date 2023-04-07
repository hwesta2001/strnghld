using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgacInit : MonoBehaviour
{
    [SerializeField] List<GameObject> childAgaclar = new List<GameObject>();

    void OnEnable()
    {
        //IlkMetot();
        IkýncýMetot();
    }


    void IkýncýMetot()
    {
        childAgaclar.Clear();
        foreach (Transform child in transform)
        {
            childAgaclar.Add(child.gameObject);
        }

        int a = Random.Range(0, childAgaclar.Count);
        for (int i = 0; i < childAgaclar.Count; i++)
        {
            if (i != a)
            {
                Destroy(childAgaclar[i].gameObject);
            }
        }
        childAgaclar.Clear();
    }

    void IlkMetot()
    {
        childAgaclar.Clear();
        foreach (Transform child in transform)
        {
            childAgaclar.Add(child.gameObject);
        }

        if (childAgaclar.Count == 1)
        {
            childAgaclar.Clear();
            return;
        }

        int a = Random.Range(0, childAgaclar.Count);
        GameObject go = childAgaclar[a];
        childAgaclar.Remove(go);

        foreach (var item in childAgaclar)
        {
            Destroy(item);
        }
        childAgaclar.Clear();
    }
}
