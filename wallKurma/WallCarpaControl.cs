using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WallCarpaControl : MonoBehaviour
{

    [SerializeField] Transform[] points = new Transform[4];

    [SerializeField] float radius = .001f;

    bool canUpdate;
    LayerMask layerMask;
   

    private void Start()
    {
        layerMask = Globals.WALL_KURMA.wallCantCollideLayers;
    }

    void OnEnable()
    {
        canUpdate = true;
        gameObject.layer = LayerMask.NameToLayer("wallPrefab");
        if (Globals.WALL_KURMA)
            Globals.WALL_KURMA.wallKuruldu += CantUpdateThis;
    }

    void OnDisable()
    {
        if (Globals.WALL_KURMA)
            Globals.WALL_KURMA.wallKuruldu -= CantUpdateThis;
    }

    void CantUpdateThis()
    {
      
        gameObject.layer = LayerMask.NameToLayer("Default"); // or building.
        canUpdate = false;
        Globals.WALL_KURMA.wallKuruldu -= CantUpdateThis;

    }

    void Update()
    {
        CarmaUpdate();
    }

    void CarmaUpdate()
    {
        if (!canUpdate) return;
        if (Globals.WALL_KURMA.halatCarpiyor) return;
        if (CarpmaControl())
        {
            Globals.WALL_KURMA.CarpmaAyarla(true, Color.red);
            
        }
        else
        {
            Globals.WALL_KURMA.CarpmaAyarla(false, Color.green);
           
        }

    }

    bool CarpmaControl()
    {
        
        for (int i = 0; i < points.Length; i++)
        {
            if (Physics.CheckSphere(points[i].position, radius, layerMask))
            {
                return true;
            }
        }
        return false;

    }

    //void OnDrawGizmos()
    //{
    //    for (int i = 0; i < points.Length; i++)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawSphere(points[i].position, radius);
    //    }
    //}
}
