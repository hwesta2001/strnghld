using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class Globals : MonoBehaviour
{
    public Transform DebugTop;

    public static Globals ins;
    public Joystick joy;

    // Ana chachelenecek global veriler(kamera player vs burdan alýnacak-)
    public Camera mainCamera;
    public Transform PLAYER;
    public GameObject insaatAlani;
    public static CharacterController playerController;

    //Global static Classlar aþaðýda
    public static WallKurma WALL_KURMA;

    // PUBLIC ACTIONLAR ASAGIDA
    public static Action GuiRefresh; // GUI Refresh yapmak icin bu actionu kullan
    public static Action<GloabalResource> DepoSetter; // ResourceDepoSetter


    public GameObject isciPrefab;  // isci spawný için global prefab
    public Transform Inn;

    public bool nowBuilding;

    [HideInInspector] // touch camera kontrol ayarlarýný tweeklemek icindi.
    public float sens = 0.01f, smoot = 3.55f, kat = 0.75f, ara = 1.01f;
    [HideInInspector]
    public float insaaUzakligi = 12f;

    public int Gold { get; set; }
    public int Wood { get; set; }
    public int Stone { get; set; }
    public int Iron { get; set; }
    public int Food { get; set; }
    public int Weath { get; set; }
    public int Flour { get; set; }
    public int MaxPopulation { get; set; }
    public int Population { get; set; }
    public int Happines { get; set; }

    [HideInInspector] public int woodDegisimMiktari;
    [HideInInspector] public int stoneDegisimMiktari;   //henuz eklenmedi
    [HideInInspector] public int ironDegisimMiktari;    //henuz eklenmedi
    [HideInInspector] public int flourDegisimMiktari;   //henuz eklenmedi
    [HideInInspector] public int wheatDegisimMiktari;   //henuz eklenmedi
    // [HideInInspector] public int woodDegisimMiktari;  // bu sekilde deðiþim miktarlarýný burdan cek

    InitValues initValues;
    void Awake()
    {
        if (ins != null && ins != this)
            Destroy(gameObject);
        else
            ins = this;


        //Pools.ClearAllPools(); // scene loadera taþýndý
        PLAYER = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = PLAYER.GetComponent<CharacterController>();
        initValues = GetComponent<InitValues>();
        mainCamera = Camera.main;
        insaatAlani = Resources.FindObjectsOfTypeAll<InsaaAlanControl>()[0].gameObject;
        initValues = GameObject.FindObjectOfType<InitValues>();
    }
    void Start()
    {
        Gold = initValues.initGold;
        Wood = initValues.initWood;
        Stone = initValues.initStone;
        Iron = initValues.initIron;
        Food = initValues.initFood;
        Population = initValues.initPopulation;
        MaxPopulation = initValues.initMaxPopulation;
        Flour = initValues.initFlour;
        Weath = initValues.initWeath;

        //TextControl.ins.RefreshResurceUI();
        GuiRefresh?.Invoke();

        Inn = FindObjectOfType<Inn>().transform;

        woodDegisimMiktari = initValues.woodDegisimMiktari;

    }

    public void SetGlobals(GloabalResource globalResource, int degisimMiktari)
    {
        switch (globalResource)
        {
            case GloabalResource.Gold:
                Gold += degisimMiktari;
                break;
            case GloabalResource.Wood:
                Wood += degisimMiktari;
                break;
            case GloabalResource.Stone:
                Stone += degisimMiktari;
                break;
            case GloabalResource.Iron:
                Iron += degisimMiktari;
                break;
            case GloabalResource.Food:
                Food += degisimMiktari;
                break;
            case GloabalResource.MaxPopulation:
                MaxPopulation += degisimMiktari;
                break;
            case GloabalResource.Population:
                Population += degisimMiktari;
                break;
            case GloabalResource.Happines:
                Happines += degisimMiktari;
                break;
            case GloabalResource.Weath:
                Weath += degisimMiktari;
                break;
            case GloabalResource.Flour:
                Flour += degisimMiktari;
                break;
            default:
                break;
        }
        //TextControl.ins.RefreshResurceUI();
        GuiRefresh?.Invoke();
        OnDepoSetter(globalResource);
    }

    public void OnDepoSetter(GloabalResource gr)
    {
        DepoSetter?.Invoke(gr);
    }

    public GameObject BinaKur(GameObject prefab, Transform insaat, float? time = null)
    {
        Vector3 rot = Extantions.NesneyeBak90(mainCamera.transform);
        GameObject go = Instantiate(prefab, insaat.position, Quaternion.Euler(rot));
        //float a = .3f;
        //float _time = time ?? 4f;
        //go.transform.localScale *= a;
        //Vector3 scaleSize = Vector3.one;
        //go.transform.DOScale(scaleSize, _time).SetEase(Ease.InOutBounce);
        return go;
    }

    public void PlayerTeleport(float? delay = null)
    {
        playerController.enabled = false;
        Invoke("CharControllerEnable", delay ?? .05f);
    }

    void CharControllerEnable()
    {
        playerController.enabled = true;
    }
}

