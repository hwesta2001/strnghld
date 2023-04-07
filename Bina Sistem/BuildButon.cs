using UnityEngine;

public class BuildButon : MonoBehaviour
{
    InsaaAlanControl bt;
    void Start()
    {
        bt = GetComponentInParent<InsaaAlanControl>();
    }


    public void BinaKur(Bina bina) // buton eventta bina scpritObject atan�yor
    {
        if (!InsaaEdilebilir.InsaaEtmeKontrol(bina)) return;
        bt.BinaCikar(bina.prefab, bina.buildTime);
    }

}
