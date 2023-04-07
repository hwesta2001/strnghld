using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsaaAlanControl : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] LayerMask stoneLayerMask;
    [SerializeField] int maksimunInsaaAra = 55; // mause fallow yaparken uzaga kurulumu engellemek icindi. mause fallow iptal ettik.
    [SerializeField] bool canBuild;
    [SerializeField] GameObject cubuk;
    [SerializeField] Color ilkRenk = Color.white;
    [SerializeField] Color sonRenk = Color.red;
    [SerializeField] Transform[] areaControlPoints;

    public Transform cekilenBinaTransform;
    //public BuildingControl buildingControl;

    float buildTime;
    Renderer[] rend;
    bool tik;  // statatelerdeki tek trigerlýk actionlar icin.
    bool canUpdate;
    KafaCanvas kf;
    //Rigidbody rb;
    float insaUzakligi;
    [SerializeField]
    BuildButtonsControl bbControl;

    public enum InsaaAlaniState { Drag, BuildingNow, BuildDone }
    public InsaaAlaniState insaaState;
    bool carpti;

    private void Awake()
    {
        rend = GetComponentsInChildren<MeshRenderer>();
        //rb = GetComponent<Rigidbody>();
        canUpdate = false;
    }

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        player = Globals.ins.PLAYER.transform;
        insaUzakligi = Globals.ins.insaaUzakligi;
        //yield return new WaitForSeconds(0.6f);
        bbControl = Resources.FindObjectsOfTypeAll<BuildButtonsControl>()[0];
        gameObject.SetActive(false);
        canUpdate = true;
    }

    private void OnEnable()
    {
        insaaState = InsaaAlaniState.Drag;
    }
    private void OnDisable()
    {
        insaaState = InsaaAlaniState.Drag;
    }
    void Update()
    {
        if (!canUpdate) return;
        switch (insaaState)
        {
            case InsaaAlaniState.Drag:
                Draging();
                //Debug.Log("Drag State");

                break;
            case InsaaAlaniState.BuildingNow:
                BuildingNow();
                //Debug.Log("First State");

                break;
            case InsaaAlaniState.BuildDone:
                DoneNow();
                //Debug.Log("Second State");

                break;
        }

        //Debug.Log("tik = " + tik);
    }

    void Draging()
    {
        Globals.ins.nowBuilding = true;

        if (carpti)
        {
            CanBuildFlash(canBuild = false);
        }
        else
        {
            CanBuildFlash(canBuild = true);
        }

        FallowMe();
        BuildAndGoFirstState();

    }

    void FallowMe()
    {
        if (player == null) return;
        transform.position = Extantions.Onunde(player.transform, insaUzakligi);
        transform.localEulerAngles = Extantions.NesneyeBak90(player.transform);

    }

    private void BuildAndGoFirstState()
    {
        if (PlayerInput.BuildHandler())
        {
            if (canBuild)
            {
                ColorlarReset();
                insaaState = InsaaAlaniState.BuildingNow;
                tik = true;
            }
            else
            {
                string str1 = "Cant build there M'Lord!";
                TextControl.ins.AlertText(str1, 37, Color.red, 1f);
            }

        }
    }


    void BuildingNow()
    {

        if (insaaState != InsaaAlaniState.BuildingNow) return;
        if (!tik) return;
        tik = false; //burdan alttakiler tek seferlik trigger olacak functionlar.
        //ColliderChange();
        gameObject.tag = "Building";
        KafaActive();
        Globals.ins.nowBuilding = false;
        bbControl.ActiveHaleGeriDon();

    }

    void DoneNow()
    {
        if (insaaState != InsaaAlaniState.BuildDone) return;
        if (!tik) return;
        tik = false; //burdan alttakiler tek seferlik trigger olacak functionlar.
        //buildingControl.WaitForInsaa();
        cubuk.transform.DORotateY(buildTime, 3, Ease.Linear);
        gameObject.SetActive(false);
    }


    void KafaActive()
    {
        kf = GetComponent<KafaCanvas>();
        kf.KafaCanvasInit();
        kf.stoneArea = AreaControl(stoneLayerMask); // ironArea da da bunun aynýsý olacak

    }

    public void BinaCikar(GameObject prefab, float time)
    {
        //GameObject go = Extantions.BinaKur(prefab, transform, time);
        Vector3 rot = Extantions.NesneyeBak90(Globals.ins.mainCamera.transform);
        GameObject go = Instantiate(prefab, transform.position, Quaternion.Euler(rot));
        buildTime = time;
        cekilenBinaTransform = go.transform;
        kf.KafaCanvasDeatvie();
        tik = true;
        insaaState = InsaaAlanControl.InsaaAlaniState.BuildDone;

        //buildingControl = cekilenBinaTransform.GetComponent<BuildingControl>();
        //if (buildingControl == null) return;
        //buildingControl.needWorker = false;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Building") || other.CompareTag("Agac"))
        {
            carpti = true;

        }
        if (other.CompareTag("StoneArea")) // ironArea da da bunun aynýsý olacak
        {
            carpti = !AreaControl(stoneLayerMask);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building") || other.CompareTag("Agac") || other.CompareTag("StoneArea"))
        {
            carpti = false;

        }
    }


    bool AreaControl(LayerMask _layer)
    {
        bool donder = false;
        int a = 0;
        for (int i = 0; i < areaControlPoints.Length; i++)
        {

            Transform trans = areaControlPoints[i];
            Ray ray = new Ray(trans.position, Vector3.down * 100);
            Debug.DrawRay(trans.position, Vector3.down * 100, Color.red);
            if (Physics.Raycast(ray, 20f, _layer))
            {
                a++;
            }
            if (a >= 4)
            {
                donder = true;
            }
        }

        return donder;

    }



    void CanBuildFlash(bool can)
    {
        if (can)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                foreach (var item in rend[i].materials)
                {
                    //item.color = Extantions.ColorTick(ilkRenk, Color.green, .5f);
                    item.color = Color.green;
                }
            }
        }
        else
        {
            for (int i = 0; i < rend.Length; i++)
            {
                foreach (var item in rend[i].materials)
                {
                    //item.color = Extantions.ColorTick(ilkRenk, sonRenk, .4f);
                    item.color = Color.red;
                }
            }
        }
    }

    void ColorlarReset()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            foreach (var item in rend[i].materials)
            {
                item.color = Color.grey;
            }
        }
    }
}
