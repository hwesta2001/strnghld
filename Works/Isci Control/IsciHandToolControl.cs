using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsciHandToolControl : MonoBehaviour
{
    [SerializeField] Transform rightHand;
    [SerializeField] GameObject bosObje;        // objelieri inspecterden tek tek ekleyecegiz.
    [SerializeField] GameObject pickaxe;
    [SerializeField] GameObject tirpan;
    [SerializeField] GameObject balta;
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject kilic;           // diger tum objeleri ekleyebiliriz.

    //debug icin serialize
    [SerializeField] HashSet<GameObject> allHandItems = new();

    Isci isci;
    IsciGorevi currentIsciGorevi;

    private void OnEnable()
    {
        if (rightHand == null)
            rightHand = transform.FindAChild("Bip001 R Hand");

        isci = GetComponent<IsciControl>().isci;
        currentIsciGorevi = isci.gorev;
        allHandItems.Add(bosObje);
        allHandItems.Add(pickaxe);
        allHandItems.Add(tirpan);
        allHandItems.Add(balta);
        allHandItems.Add(hammer);
        allHandItems.Add(kilic);
        FindAndPut();
        SetItem(bosObje);
    }


    void FindAndPut()
    {

        foreach (var item in allHandItems)
        {
            item.transform.parent = rightHand;
            item.transform.localPosition = Vector3.zero;
        }

    }

    private void SetItem(GameObject go)
    {
        foreach (var item in allHandItems)
        {
            item.SetActive(false);
        }
        go.SetActive(true);
    }

    private void LateUpdate()
    {
        if (currentIsciGorevi == isci.gorev) return;
        currentIsciGorevi = isci.gorev;
        ChangeItem();
    }

    private void ChangeItem()
    {
        switch (currentIsciGorevi)
        {
            case IsciGorevi.Idle:
                SetItem(bosObje);
                break;
            case IsciGorevi.Oduncu:
                SetItem(balta);
                break;
            case IsciGorevi.TasUstasi:
                SetItem(pickaxe);
                break;
            case IsciGorevi.Wagoncu:
                SetItem(bosObje);
                break;
            case IsciGorevi.Madenci:
                SetItem(hammer);
                break;
            case IsciGorevi.WheatFarmer:
                SetItem(tirpan);
                break;
            case IsciGorevi.AppleFarmer:
                SetItem(tirpan);
                break;
            case IsciGorevi.Asker:
                SetItem(kilic);
                break;
            case IsciGorevi.NonWorker:
                SetItem(bosObje);
                break;
            case IsciGorevi.MillWorker:
                SetItem(bosObje);
                break;
            case IsciGorevi.Bakery:
                SetItem(bosObje);
                break;
        }
    }

}
