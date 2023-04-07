using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardToCam : MonoBehaviour
{
    [SerializeField] Transform bilboard;
    [SerializeField] Transform cam; // cachleyebiliriz de biryerden!!!

    void Start()
    {
        if (cam == null)
        {
            cam = Globals.ins.mainCamera.transform;
        }
    }


    void LateUpdate()
    {
        //Vector3 targetPostition = new Vector3(cam.position.x, bilboard.position.y, cam.position.z);
        //bilboard.LookAt(targetPostition);
        bilboard.forward = -cam.forward;
    }

}
