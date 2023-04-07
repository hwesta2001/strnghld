using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// unitHp Barý unitin childý olmak zorunda!!!!
public class UnitHpBar : MonoBehaviour
{
    Unit unit;
    float lastHp;
    [SerializeField] Transform bar;
    [SerializeField] Renderer rend;
    Transform cam;
    void OnEnable()
    {
        unit = GetComponentInParent<UnitMono>().unit;
        lastHp = unit.hp;
        SetBar();
        GetComponentInParent<UnitMono>().selectLocation = transform;
        cam = Camera.main.transform;  // globalsten cek!!
        //rend = bar.GetComponentInChildren<MeshRenderer>();
    }

    void LateUpdate()
    {
        LookAtCam();
        if (lastHp != unit.hp)
        {
            SetBar();
            lastHp = unit.hp;
        }
    }

    void SetBar()
    {
        float lenght = unit.hp / unit.maxHp;
        bar.localScale = new Vector3(lenght, 1.001f, 1.001f);
        Color col = Color.green;
        if (lenght < 0.5f && lenght >= 0.3f)
        {
            col = Color.yellow;
        }
        if (lenght < 0.3f)
        {
            col = Color.red;
        }
        rend.material.color = col;
    }

    void LookAtCam()
    {
        Vector3 targetPostition = new Vector3(cam.position.x, transform.position.y, cam.position.z);
        transform.LookAt(targetPostition);
    }


}
