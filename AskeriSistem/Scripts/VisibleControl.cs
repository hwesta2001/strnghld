using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class VisibleControl : MonoBehaviour
{
    [SerializeField] MonoBehaviour[] mono;

    // scene view de visiable say�l�yor �a���rtmas�n!!!
    void OnBecameInvisible()
    {
        foreach (MonoBehaviour item in mono)
        {
            item.enabled = false;
        }

    }

    void OnBecameVisible()
    {
        foreach (MonoBehaviour item in mono)
        {
            item.enabled = true;
        }
    }
}
