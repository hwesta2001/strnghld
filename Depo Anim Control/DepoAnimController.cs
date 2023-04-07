using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DepoAnimController : MonoBehaviour
{
    [Tooltip("Depo Cinsine göre kaynaklarý alýp istifler.")]
    [SerializeField] DepoCinsi depoCinsi;
    public List<Culuster> woodCulusters = new();
    public List<Culuster> stoneCulusters = new();
    public List<Culuster> ironCulusters = new();
    public List<Culuster> flourCulusters = new();


    private void OnEnable()
    {

        StartCoroutine(InitDisableAll());

        if (depoCinsi == DepoCinsi.Resource)
        {
            Globals.DepoSetter += SetDepo;
        }
        else if (depoCinsi == DepoCinsi.Food)
        {

        }
        else if (depoCinsi == DepoCinsi.Barn)
        {

        }
        else if (depoCinsi == DepoCinsi.Armory)
        {

        }
    }


    private void OnDisable()
    {
        if (depoCinsi == DepoCinsi.Resource)
        {
            Globals.DepoSetter -= SetDepo;
        }
        else if (depoCinsi == DepoCinsi.Food)
        {

        }
        else if (depoCinsi == DepoCinsi.Barn)
        {

        }
        else if (depoCinsi == DepoCinsi.Armory)
        {

        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.9f);
        for (int i = 0; i < (int)GloabalResource.LastEnum; i++)
        {
            Globals.ins.OnDepoSetter((GloabalResource)i);
        }
        //Globals.ins.OnDepoSet(GloabalResource.Wood);
    }

    void SetDepo(GloabalResource gloabalResource)
    {
        if (depoCinsi == DepoCinsi.Resource)
        {
            DepoIstif(gloabalResource);
        }
        else if (depoCinsi == DepoCinsi.Food)
        {
            // food deposunu istifle FoodDepoIstif(gloabalResource);
        }
        else if (depoCinsi == DepoCinsi.Barn)
        {
            // barn deposunu istifle BarnIstif(gloabalResource);
        }
        else if (depoCinsi == DepoCinsi.Armory)
        {
            // armory deposunu istifle ArmoryIstif(gloabalResource);
        }
    }

    private void DepoIstif(GloabalResource gloabalResource)
    {
        // public enum GloabalResource { Wood, Stone, Iron, Flour,}
        if (gloabalResource == GloabalResource.Wood)
        {
            if (woodCulusters.Count <= 0) return;
            CulusterIstif(woodCulusters, Globals.ins.Wood);
        }
        else if (gloabalResource == GloabalResource.Stone)
        {
            if (stoneCulusters.Count <= 0) return;
            CulusterIstif(stoneCulusters, Globals.ins.Stone);
        }
        else if (gloabalResource == GloabalResource.Iron)
        {
            if (ironCulusters.Count <= 0) return;
            CulusterIstif(ironCulusters, Globals.ins.Iron);
        }
        else if (gloabalResource == GloabalResource.Flour)
        {
            if (flourCulusters.Count <= 0) return;
            CulusterIstif(flourCulusters, Globals.ins.Flour);
        }
    }


    [ContextMenu("Disable Culuster")]
    IEnumerator InitDisableAll()
    {
        yield return new WaitForEndOfFrame();
        DisableCuluster(woodCulusters);
        DisableCuluster(stoneCulusters);
        DisableCuluster(ironCulusters);
        DisableCuluster(flourCulusters);
    }

    void DisableCuluster(List<Culuster> culuster)
    {
        if (culuster.Count <= 0) return;
        foreach (var _item in culuster)
        {
            foreach (var item in _item.blocks)
            {
                item.SetActive(false);
            }
        }
    }


    void CulusterIstif(List<Culuster> culuster, int amount)
    {
        DisableCuluster(culuster);
        int blockCount = culuster[0].blocks.Count;
        int blockStackCount = culuster[0].anaBlockYukseltiAdedi * blockCount;
        int blockTotalCount = blockStackCount + blockCount;

        if (amount >= blockTotalCount * culuster.Count)
        {
            foreach (var item in culuster)
            {
                CulusturleriAktifet(item);
            }
            return;
        }
        else
        {

            int culusterSayisi = Mathf.FloorToInt(amount / blockTotalCount);
            int kalan = amount - culusterSayisi * blockTotalCount;
            int yükseklik = Mathf.FloorToInt(kalan / blockCount);
            int kalanBlock = kalan - yükseklik * blockCount;

            //Debug.LogError(culusterSayisi + " culusterSayisi");
            //Debug.LogError(kalan + " kalan");
            //Debug.LogError(yükseklik + " yükseklik");
            //Debug.LogError(kalanBlock + " kalanBlock");




            if (culusterSayisi > 0)
            {
                for (int i = 0; i < culusterSayisi; i++)
                {
                    CulusturleriAktifet(culuster[i]);
                }
                RiseBlocks(culuster[culusterSayisi], yükseklik);
                BlockAktif(culuster[culusterSayisi], kalanBlock);
            }
            else
            {
                RiseBlocks(culuster[culusterSayisi], yükseklik);
                BlockAktif(culuster[culusterSayisi], kalanBlock);
            }
        }

    }


    void CulusturleriAktifet(Culuster culuster)
    {
        RiseBlocks(culuster, culuster.anaBlockYukseltiAdedi);
        BlockAktif(culuster, culuster.blocks.Count);
    }

    void BlockAktif(Culuster culuster, int adet)
    {
        foreach (var item in culuster.blocks)
        {
            item.SetActive(false);
        }
        for (int i = 0; i < adet; i++)
        {
            culuster.blocks[i].SetActive(true);
        }
    }

    void RiseBlocks(Culuster cul, int adet)
    {
        cul.anablock.localPosition = cul.riseRate * adet * Vector3.up;
        foreach (var item in cul.blocks)
        {
            item.transform.localPosition = cul.riseRate * adet * Vector3.up;
        }
    }

}

