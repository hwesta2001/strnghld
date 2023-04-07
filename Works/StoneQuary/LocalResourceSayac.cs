using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LocalResourceSayac : MonoBehaviour
{
    public int adet=0;
    [SerializeField] TextMeshPro text;
    private void OnEnable()
    {
        SetLocalStone(0);
    }
    public void SetLocalStone(int amount)
    {
        adet += amount;
        adet = Mathf.Clamp(adet, 0, 90);
        text.text = adet.ToString();
    }

}
