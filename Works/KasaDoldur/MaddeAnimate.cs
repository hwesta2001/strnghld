using System.Collections;
using UnityEngine;

public class MaddeAnimate : MonoBehaviour
{
    [SerializeField] GameObject[] maddeler;
    [SerializeField] float hiz = .2f;
    public int maxMaddeSayisi; // maddelerin sayýsý ile diðer yerden fulleme yapýlýr.
    public bool doldu;


    int i, x;
    int kere;
    IEnumerator currentie, lastie, bosalIe;
    bool bosaliyor;
    void OnEnable()
    {
        maxMaddeSayisi = maddeler.Length;
        bosaliyor = false;
        bosalIe = TekerTekerDeAktif(maxMaddeSayisi);
    }

    public void GetItems(int adet)
    {
       
        if (i <= 0)
        {
            DisableAll();
        }
        bosaliyor = false;
        kere += adet;
        kere = Mathf.Clamp(kere, 0, maxMaddeSayisi);
        if (i >= maxMaddeSayisi) return;
        StopCoroutine(bosalIe);
        currentie = TekerTekerAktif();
        if (currentie == lastie)
        {
            StopCoroutine(lastie);
        }
        StartCoroutine(currentie);
    }

    IEnumerator TekerTekerAktif()
    {

        GameObject go = maddeler[i];
        go.SetActive(true);
        i++;
        yield return new WaitForSeconds(hiz);
        if (i < maxMaddeSayisi)
        {
            doldu = false;
            if (i < kere)
            {
                lastie = TekerTekerAktif();
                StartCoroutine(lastie);
            }
        }
        else
        {
            doldu = true;
            kere = i;
            yield return null;
        }

    }

    public void DisableAll()
    {
        foreach (GameObject item in maddeler)
        {
            i = 0;
            kere = 0;
            x = 0;
            item.SetActive(false);
        }
    }


    public void FullBosalt()
    {
        if (ActiveSayi <= 0) return;
        if (bosaliyor) return;
        x = 0;
        StartCoroutine(TekerTekerDeAktif(ActiveSayi - 1));
    }

    IEnumerator TekerTekerDeAktif(int adet)
    {
        bosaliyor = true;
        maddeler[adet - x].SetActive(false);
        i = adet - x;
        kere = i;
        x++;
       
        yield return new WaitForSeconds(hiz);
        if (x <= adet)
        {
           
            StartCoroutine(TekerTekerDeAktif(adet));
        }
        else
        {
            doldu = false;
            x = 0;
            bosaliyor = false;
            yield return null;
        }

    }


    public int ActiveSayi
    {
        get
        {

            int a = 0;
            for (int i = 0; i < maddeler.Length; i++)
            {
                if (maddeler[i].activeInHierarchy == true)
                {
                    a++;
                }
            }

            return a;
        }
    }
}
