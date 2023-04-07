using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KafaCanvas : MonoBehaviour
{
    [SerializeField] InsaaAlanControl bt;
    [SerializeField] Canvas canvas;
    [SerializeField] Transform kafa;
    [SerializeField] GameObject panel;
    [SerializeField] Button buton;
    [SerializeField] Sprite kafaButonSprite;
    [SerializeField] Bina stoneQuaryBina;



    public bool stoneArea;
    public bool ironArea;

    IEnumerator butonChanger;
    Image kafaButonu;
    //MeshRenderer rend;
    Camera cam;
    bool active;
    bool yakin;

    float posY, currentYpos;
    Vector3 rotX = Vector3.zero;
    Vector3 initScale, topScale;

    private void Awake()
    {
        //rend = kafa.GetComponent<MeshRenderer>();
        initScale = kafa.localScale;

    }

    void OnEnable()
    {
        canvas.gameObject.SetActive(false);
        butonChanger = ButonChanger();
        currentYpos = 3.1f;
    }
    void Update()
    {
        if (!active) return;
        KamerayaDon();
        KafaScale();
        if (!DistanceCheck())
        {

            if (!yakin) return;
            yakin = false;
            ButonActive();
            //rend.material.color = Color.white;
        }
        else
        {

            if (yakin) return;
            yakin = true;
            //rend.material.color = Color.green;
        }
    }



    IEnumerator ButonChanger()
    {
        yield return new WaitForEndOfFrame();

        if (stoneArea)
        {
            ButonIconSet(stoneQuaryBina.icon, Color.white);
        }
        else
        {
            ButonIconSet(kafaButonSprite, Color.white);
        }
    }



    public void KafaCanvasInit()
    {
        KafaCanvasActive();

        if (cam != null) return;
        cam = Globals.ins.mainCamera;
        canvas.worldCamera = cam;

    }

    public void KafaCanvasActive()
    {
        canvas.gameObject.SetActive(true);
        active = true;

        ButonActive();

    }

    public void KafaCanvasDeatvie()
    {
        ButonActive();
        canvas.gameObject.SetActive(false);
        active = false;
    }
    public void ButonActive()
    {
        if (butonChanger != null)
        {
            StopCoroutine(butonChanger);

        }
        panel.SetActive(false);
        buton.gameObject.SetActive(true);
        StartCoroutine(ButonChanger());

    }
    public void PanelActiveButon()
    {
        if (!DistanceCheck()) return;
        if (stoneArea)
        {
            if (!InsaaEdilebilir.InsaaEtmeKontrol(stoneQuaryBina)) return;
            InsaaEt(stoneQuaryBina.prefab, stoneQuaryBina.buildTime);
            return;
        }
        if (ironArea)
        {
            // ironMinePrefabý eklenecek
            if (!InsaaEdilebilir.InsaaEtmeKontrol(stoneQuaryBina)) return;
            InsaaEt(stoneQuaryBina.prefab, stoneQuaryBina.buildTime);

            return;
        }

        buton.gameObject.SetActive(false);
        panel.SetActive(true);

    }

    void InsaaEt(GameObject go, float time)
    {
        buton.gameObject.SetActive(false);
        bt.BinaCikar(go, time);
    }

    private void ButonIconSet(Sprite sprite, Color color)
    {
        bool butonAC = buton.gameObject.activeSelf;
        buton.gameObject.SetActive(true);
        kafaButonu = buton.GetComponent<Image>();
        kafaButonu.gameObject.SetActive(true);
        kafaButonu.sprite = sprite;
        kafaButonu.color = color;
        buton.gameObject.SetActive(butonAC);

    }

    void KafaScale()
    {

        if (GlobalVeriler.CAMERA_STATE == CameraState.Top1)
        {
            posY = 5.25f;
            rotX = new Vector3(-90, 0, 0);
            topScale = initScale * 1.4f;
        }
        else // CAMERA_STATE == CameraState.FPS ve init neyse iþte ...
        {
            posY = 3.1f;
            rotX = Vector3.zero;
            topScale = initScale;
        }
        if (currentYpos == posY) return;
        currentYpos = posY;
        kafa.DOLocalMoveY(posY, 1f);
        kafa.DOLocalRotate(rotX, 1f);
        kafa.DOScale(topScale, 1f);
    }

    private void KamerayaDon()
    {
        //if (GlobalVeriler.CAMERA_STATE != CameraState.Fps) return;
        Vector3 targetPostition = cam.transform.position;

        if (GlobalVeriler.CAMERA_STATE == CameraState.Fps)
        {
            targetPostition = new Vector3(cam.transform.position.x, kafa.position.y, cam.transform.position.z);
        }
        kafa.LookAt(targetPostition);
    }

    bool DistanceCheck()
    {
        if (GlobalVeriler.CAMERA_STATE != CameraState.Fps) return true;
        Vector3 disVec = Globals.ins.PLAYER.position - transform.position;
        float d = Vector3.SqrMagnitude(disVec); // aradaki uzaklýðýn karesi
        float ara = Globals.ins.insaaUzakligi * Globals.ins.insaaUzakligi; // ara=12*12=144f !!
        if (d > ara + 25f) // 144+25 = 169 yani 13karesi
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}