using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallInitBuild : MonoBehaviour
{
    Transform cam;
    [SerializeField] Transform hall;
    [SerializeField] Transform kafa, kafaFallow;
    [SerializeField] IsciSpawn isciSpawn;
    bool playerFreeze;
    //[SerializeField] Collider[] hallColliders;
    [SerializeField] GameObject[] kurulumSonrasiAktifOlacaklar;
    bool active;
    Renderer[] yesilKafa;
    CharacterController playerController;
    Transform player;
    BoxCollider boxCollider;

    void Start()
    {
        isciSpawn.enabled = true; // isciSpawna baþlama, starttan kaldýr, false yap.

        boxCollider = GetComponent<BoxCollider>();

        player = Globals.ins.PLAYER;

        foreach (GameObject item in kurulumSonrasiAktifOlacaklar)
        {
            item.SetActive(false);
        }
        hall.gameObject.SetActive(false);
        hall.localScale = 0.02f * Vector3.one;
        cam = Camera.main.transform;
        yesilKafa = kafa.GetComponentsInChildren<MeshRenderer>();
        playerController = Globals.playerController;
        if (playerController == null)
        {
            //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
            Debug.Log(playerController + " yok");
        }

        boxCollider.enabled = false;

    }
    void OnEnable()
    {
        active = true;
    }
    void Update()
    {

        KafaDondur();
        MouseClickKontrol();
        if (!playerFreeze) return;
        player.position = new Vector3(player.position.x, kafaFallow.position.y + .01f, player.position.z);

        //playerController.transform.position = new Vector3(playerController.transform.position.x, kafaFallow.position.y + .01f, playerController.transform.position.z);

    }

    void MouseClickKontrol()
    {
        if (!active) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerInput.MouseHitOnObject(kafa))
                HallSpawnBegin();
        }

    }


    void KafaDondur()
    {
        if (kafaFallow.gameObject.activeInHierarchy)
        {
            kafa.localPosition = new Vector3(kafa.localPosition.x, kafaFallow.position.y, kafa.localPosition.z);
        }
        if (!active) return;
        Vector3 targetPostition = new Vector3(cam.transform.position.x, kafa.position.y, cam.transform.position.z);

        kafa.LookAt(targetPostition);
        foreach (var item in yesilKafa)
        {
            item.material.color = Extantions.ColorTick(Color.green, Color.red);
        }
    }

    public void HallSpawnBegin()
    {
        playerFreeze = true;
        playerController.enabled = false;

        active = false;
        foreach (GameObject item in kurulumSonrasiAktifOlacaklar)
        {
            item.SetActive(true);
        }
        float buildTime = 3;
        kafa.DORotateY(buildTime, 2, Ease.InOutElastic, func: HallSpawnEnd);
        hall.gameObject.SetActive(true);
        hall.DOScale(Vector3.one, buildTime).SetEase(Ease.InOutBounce);
    }

    void HallSpawnEnd()
    {
        kafa.DOScale(Vector3.one * 0.01f, 1).SetEase(Ease.InBounce).OnComplete(DestroyKafa);
    }

    void DestroyKafa()
    {
        DOTween.KillAll();
        playerFreeze = false;
        playerController.enabled = true;
        isciSpawn.enabled = true;
        boxCollider.enabled = true;
        Destroy(this, .1f);
        Destroy(kafa.gameObject, .1f);
    }
}
