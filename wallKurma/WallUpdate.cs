using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WallUpdate : MonoBehaviour
{
    protected Transform cam;
    protected float insaaUzakl�gi;
    protected WallCarpaControl wallCarpaControl;
    public bool onlyRotate;
    public int wallState;

    void Start()
    {
        cam = Globals.ins.mainCamera.transform;
        insaaUzakl�gi = Globals.ins.insaaUzakligi;
    }

    protected void RotateDuvara(Transform tr)
    {
        Vector3 target = new Vector3(tr.position.x, transform.position.y, tr.position.z);
        transform.LookAt(target);
    }

    protected void GridMove()
    {
        Vector3 point = Extantions.Onunde(cam.transform, insaaUzakl�gi);
        transform.position = point;
    }
}
