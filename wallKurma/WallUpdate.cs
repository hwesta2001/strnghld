using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallUpdate : MonoBehaviour
{
    protected Transform cam;
    protected float insaaUzaklęgi;
    protected WallCarpaControl wallCarpaControl;
    public bool onlyRotate;
    public int wallState;

    void Start()
    {
        cam = Globals.ins.mainCamera.transform;
        insaaUzaklęgi = Globals.ins.insaaUzakligi;
    }

    protected void RotateDuvara(Transform tr)
    {
        Vector3 target = new Vector3(tr.position.x, transform.position.y, tr.position.z);
        transform.LookAt(target);
    }

    protected void GridMove()
    {
        Vector3 point = Extantions.Onunde(cam.transform, insaaUzaklęgi);
        transform.position = point;
    }
}
