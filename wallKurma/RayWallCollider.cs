using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWallCollider : MonoBehaviour
{
    Camera cam;
    Ray ray;
    [SerializeField] float rayDistance = 5f;
    [SerializeField] LayerMask layerMask;
    Color col = Color.green;
    bool canUpdate = true;

    void Start()
    {
        cam = Globals.ins.mainCamera;
        rayDistance = Globals.ins.insaaUzakligi + .2f;

    }

    void Update()
    {
        RayToWall();
    }

    void RayToWall()
    {

        canUpdate = Globals.WALL_KURMA.frontColliderUpdate;

        if (!canUpdate) return;

        ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
        {
            col = Color.red;
            if (!canUpdate) return;
            Debug.Log("carpýyo");
            Globals.WALL_KURMA.WallUpdate(false, hit.collider.transform);
        }
        else
        {
            col = Color.green;
            if (!canUpdate) return;
            Globals.WALL_KURMA.WallUpdate(true, transform);
        }

    }


    void OnDrawGizmos()
    {

        Gizmos.color = col;

        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance);
    }


}
