using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// bu scripti bina prefabýna ekle. gerekli verileri inspectorden girmeyi unutma !! bina aktif olarak çaðýrýldýðýnda baþlat.

public class BinaKurTween : MonoBehaviour
{

    //[SerializeField] Transform[] objeler; // inspectorden objeleri tek tek iliþtir.
    //[SerializeField] float initY = 6.5f;
    //[SerializeField] float time = .4f;
    //Vector3[] initPos;


    [SerializeField] BuildingControl buildingControl; // inspectorden objeleri tek tek iliþtir.
    [SerializeField] Ease ease = Ease.InOutBounce;
    Vector3 initSize;


    private void OnEnable()
    {

        initSize = transform.localScale;
        transform.localScale = Vector3.one * .0001f;
        StartCoroutine(Kur());
    }

    IEnumerator Kur()
    {
        if (buildingControl == null)
        {
            buildingControl = GetComponent<BuildingControl>();
        }
        yield return new WaitForSeconds(.1f);
        Run();
    }

    void Run()
    {
        transform.DOScale(initSize, buildingControl.bina.buildTime)
            .SetEase(ease)
            .OnComplete(() => StartCoroutine(LastMetot()));
    }

    IEnumerator LastMetot()
    {
        yield return new WaitForEndOfFrame();
        buildingControl.Kuruldu();
        Debug.Log("Destryoing " + this.name);
        Destroy(this);
    }






    //void Run()
    //{
    //    time = SetTime();
    //    Sequence mySequence = DOTween.Sequence();

    //    for (int i = 0; i < objeler.Length; i++)
    //    {
    //        mySequence.Append(objeler[i].DOLocalMove(initPos[i], time).SetEase(ease));
    //    }
    //    mySequence.Play().OnComplete(() => LastMetot());
    //}



    //public void GeriAl()
    //{
    //    DOTween.KillAll();
    //    for (int i = 0; i < objeler.Length; i++)
    //    {
    //        objeler[i].localPosition = new Vector3(objeler[i].localPosition.x, -initY, objeler[i].localPosition.z);
    //    }
    //}
    //void GetInitPos()
    //{
    //    initPos = new Vector3[objeler.Length];
    //    for (int i = 0; i < objeler.Length; i++)
    //    {
    //        initPos[i] = objeler[i].localPosition;
    //    }
    //}

    //float SetTime() => buildingControl.bina.buildTime / objeler.Length;

}
