using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerdivenCollideKontrol : MonoBehaviour
{

    private void OnEnable()
    {
        Side.MerdivenKuruldu += AfterMerdivenKuruldu;
    }

    void AfterMerdivenKuruldu()
    {
        Side.MerdivenKuruldu -= AfterMerdivenKuruldu;
        Destroy(GetComponent<Rigidbody>());
        GetComponent<BoxCollider>().isTrigger = false;
        Side.merdivenColliding = false;
        Destroy(this);

    }



    private void OnDisable()
    {
        Side.MerdivenKuruldu -= AfterMerdivenKuruldu;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("wall") || other.CompareTag("merdiven")) // || other.CompareTag("building , agac vs vs eklenebilir.")
        {
            Side.merdivenColliding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wall") || other.CompareTag("merdiven"))
        {
            Side.merdivenColliding = false;
        }
    }
}
