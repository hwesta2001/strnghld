using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallCekme : MonoBehaviour
{

    WallKurma wk;
    void Start()
    {
        wk = Globals.WALL_KURMA;
        wk.StartWallCreate += WallCek;
    }

    void WallCek()
    {
        Vector3 v1 = wk.ilkDuvar.position;
        Vector3 v2 = wk.ikinciDuvar.position;

        float duvarKalinligi = 2;

        float ara = Vector3.Distance(v1, v2) / duvarKalinligi;

        int d = (int)Mathf.Floor(ara);

        // yeni =>
        if (d % 2 != 0) d += 1;
        d = (int)Mathf.Clamp(d, 2, Mathf.Infinity);
        // => yeni

        Quaternion rotation = Globals.WALL_KURMA.ikinciDuvar.localRotation;  // donme hesapla

        float yukseklik = Globals.WALL_KURMA.duvarYuksekligi;

        //Transform son = WallCikar(Globals.WALL_KURMA.ikinciDuvar.position, rotation);
        //son.DOYuksel(4, yukseklik, Ease.InOutBounce);

        Vector3 last1 = Vector3.zero;
        Vector3 last2 = Vector3.zero;

        for (int i = 0; i <= (int)(d * .5f); i++)
        {
            float x = v2.x - (i * ((v2.x - v1.x) / ara));
            float z = v2.z - (i * ((v2.z - v1.z) / ara));

            Transform tr = WallCikar(new Vector3(x, v1.y, z), rotation);

            float rnd = Random.Range(0.995f, 1.005f);
            tr.localScale = new Vector3(tr.localScale.x, tr.localScale.y * rnd, tr.localScale.z); // duvar tepeleri texture overlap olmasýn diye.

            tr.DOYuksel(yukseklik * rnd, 4, Ease.InBounce);

            if (i == (int)(d * .5f))
            {
                last2 = tr.position;
            }

        }


        for (int i = 0; i < (int)(d * .5f); i++)
        {
            float x = v1.x + (i * ((v2.x - v1.x) / ara));
            float z = v1.z + (i * ((v2.z - v1.z) / ara));

            Transform tr = WallCikar(new Vector3(x, v1.y, z), rotation);

            float rnd = Random.Range(0.995f, 1.005f);
            tr.localScale = new Vector3(tr.localScale.x, tr.localScale.y * rnd, tr.localScale.z); // duvar tepeleri texture overlap olmasýn diye.

            tr.DOYuksel(4, yukseklik, Ease.InBounce);

            if (i == (int)(d * .5f) - 1)
            {
                last1 = tr.position;
                if (Vector3.Distance(last1, last2) > duvarKalinligi)
                {
                    //float _x = v1.x + ((i + 1) * ((v2.x - v1.x) / ara));
                    //float _z = v1.z + ((i + 1) * ((v2.z - v1.z) / ara));
                    //Transform _tr = WallCikar(new Vector3(_x, v1.y, _z), rotation);

                    Vector3 orta = (last1 + last2) * .5f;
                    orta.y = 0;

                    Transform _tr = WallCikar(orta, rotation);
                    // texture overlap yapmasýn diye
                    _tr.localScale = new Vector3(_tr.localScale.x * .902f, _tr.localScale.y * 1.005f, _tr.localScale.z);
                    _tr.DOYuksel(4, yukseklik, Ease.InOutSine);
                }
            }


            //if (i == d - 1) //sonuncuyu olustururken demek bu
            //{
            //    float t = Vector3.Distance(son.position, tr.position);
            //    if (t > duvarKalinligi)
            //    {
            //        float xs = v1.x + ((i + 1) * ((v2.x - v1.x) / ara));
            //        float zs = v1.z + ((i + 1) * ((v2.z - v1.z) / ara));

            //        Transform trs = WallCikar(new Vector3(xs, v1.y, zs), rotation);
            //        float oran = 0.99f;
            //        trs.localScale = new Vector3(trs.localScale.x * oran, trs.localScale.y, trs.localScale.z * oran);
            //        trs.DOYuksel(4, yukseklik, Ease.InOutSine);
            //        trs.tag = "Untagged"; // bu tagý deðiþtirmek demek frontCollider carpmayacak buna demek sadece sonDuvara carpacak

            //    }
            //}

        }



    }

    Transform WallCikar(Vector3 point, Quaternion rot)
    {
        Transform tr = Instantiate(Globals.WALL_KURMA.wallBasNormal, point, rot).transform;
        tr.parent = transform;
        Vector3 yukseklik = new Vector3(1, Globals.WALL_KURMA.duvarYuksekligi * .2f, 1);

        tr.localScale = Vector3.Scale(yukseklik, tr.localScale);

        return tr;
    }
}
