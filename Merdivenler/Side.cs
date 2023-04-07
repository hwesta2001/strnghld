// Genel bir side detectin classý oldu bu
// Her türlü þeyde iþe yarar
// Side.Detect ( bir vector3 poitin kare bir transformun hangi yüzü tarafýnda olduðunu kolayca tespit edebiliriz.
// enum Sides döndüreceði için de 
// if (Side.Detect(point, transrom) == Sides.Left) gibi bir kontroller iþimizi halladebiriz.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sides { Left, Right, Front, Back, Up, Down }

public static class Side
{
    public static Sides Detect(Vector3 point, Transform centerObject)
    {
        Vector3 objPosition = centerObject.position;
        objPosition.y = 0;
        point.y = 0;
        var hitDirection = point - objPosition;
        hitDirection.Normalize();
        //Vector3 hitDirection = hit.point - hit.transform.position;

        //Dot products
        float upWeight = Vector3.Dot(hitDirection, centerObject.up);
        float forwardWeight = Vector3.Dot(hitDirection, centerObject.forward);
        float rightWeight = Vector3.Dot(hitDirection, centerObject.right);

        //We care about the absolute value only for now
        float upMag = Mathf.Abs(upWeight);
        float forwardMag = Mathf.Abs(forwardWeight);
        float rightMag = Mathf.Abs(rightWeight);

        if (upMag >= forwardMag && upMag >= rightMag)
        {
            if (upWeight > 0)
            {
                return Sides.Up; //Up
            }
            else
            {
                return Sides.Down; //Down
            }
        }
        else if (forwardMag >= upMag && forwardMag >= rightMag)
        {
            if (forwardWeight > 0)
            {
                return Sides.Front;//Forward
            }
            else
            {
                return Sides.Back;//Back
            }
        }
        else
        {
            if (rightWeight > 0)
            {
                return Sides.Right; //Right
            }
            else
            {
                return Sides.Left; //Left
            }
        }

    }


    // Extantionsa atýlabilir.
    public static bool CameraRayControl(this Camera cam, out RaycastHit hit, float distance, LayerMask layerMask)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        //Debug.DrawRay(ray.origin, ray.direction * distance , Color.yellow, 1f);
        if (Physics.Raycast(ray, out hit, distance, layerMask))
        {
            Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            return true;
        }

        return false;
    }

    public static Action MerdivenKuruldu;
    public static bool merdivenColliding;
}
