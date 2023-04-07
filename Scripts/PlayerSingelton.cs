using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ayni taglý bir obje varsa bu objeyi siler.
public class PlayerSingelton : MonoBehaviour
{
    public static PlayerSingelton ins;

    private void Awake()
    {
        if (ins != null && ins != this)
        {
            Destroy(gameObject);
        }
        else
        {
            ins = this;
        }
    }
}
