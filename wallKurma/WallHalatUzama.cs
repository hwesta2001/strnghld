using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHalatUzama : MonoBehaviour
{

    Transform ilkduvar, ikinciDuvar;
    MeshRenderer rend;
    LayerMask wallCantCollide;
    Vector3 origin;
    void Start()
    {
        ilkduvar = Globals.WALL_KURMA.ilkDuvar;
        ikinciDuvar = Globals.WALL_KURMA.ikinciDuvar;
        rend = GetComponentInChildren<MeshRenderer>();
        wallCantCollide = Globals.WALL_KURMA.wallCantCollideLayers;
    }

    void Update()
    {
        HalatUza();
    }
    void HalatUza()
    {
        if (Globals.WALL_KURMA.state != WallKurma.State.ikinciDuvar)
        {
            if (rend.enabled == true)
            {
                rend.enabled = false;
            }
            return;
        }


        if (rend.enabled == false)
        {
            rend.enabled = true;
        }
        float duvarArasiMesafe = Vector3.Distance(ikinciDuvar.position, ilkduvar.position);
        transform.position = ilkduvar.position;
        transform.LookAt(ikinciDuvar, Vector3.up);
        transform.localScale = new Vector3(1, 1, duvarArasiMesafe);

        BoxCastHere(duvarArasiMesafe);
    }

    void BoxCastHere(float dist)
    {
        origin = transform.position + Vector3.up * .5f;
        if (Physics.SphereCast(origin, .6f, transform.forward, out RaycastHit hit, dist, wallCantCollide))
        {
            Globals.WALL_KURMA.CarpmaAyarla(true, Color.red);
            rend.material.color = Color.red * .7f;
            Globals.WALL_KURMA.halatCarpiyor = true;
            return;
        }

        Globals.WALL_KURMA.halatCarpiyor = false;
        Globals.WALL_KURMA.CarpmaAyarla(false, Color.blue);
        rend.material.color = Color.green * .8f;

    }


}
