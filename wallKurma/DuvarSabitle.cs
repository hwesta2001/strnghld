using System.Collections;
using UnityEngine;

public static class DuvarSabitle
{

    public static void Yaslan(this Transform trans, Transform other)
    {
        trans.rotation = other.rotation;
        trans.position = other.position;

        //float duvarAralik = 1.8f;
       
        //float ara = Vector3.Distance(other.position, trans.position);
        //float d = ara / duvarAralik;
       
        //float x = other.position.x + ((trans.position.x - other.position.x) / d);
        //float z = other.position.z + ((trans.position.z - other.position.z) / d);

        //trans.position = new Vector3(x, other.position.y, z);



        //Vector3 diretion = trans.position - other.position;
        //diretion.Normalize();
        //diretion *= duvarAralik;
        //trans.position = other.position + diretion;

    }



}
