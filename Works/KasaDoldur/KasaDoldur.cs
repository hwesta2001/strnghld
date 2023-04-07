using UnityEngine;

public class KasaDoldur : MonoBehaviour
{

    public KasaMaddesi kasaMaddesi;
    [SerializeField] GameObject woodMaddePrefab, stoneMaddePrefab, ironMaddePrefab;

    MaddeAnimate woodMA, stoneMA, ironMA;
    public MaddeAnimate maddeAnim;
    GameObject dolcakOlan;


    void Start()
    {
        DisableAllPrefab();
        woodMA = woodMaddePrefab.GetComponent<MaddeAnimate>();
        stoneMA = stoneMaddePrefab.GetComponent<MaddeAnimate>();
        ironMA = ironMaddePrefab.GetComponent<MaddeAnimate>();
        dolcakOlan = GetDolacakOlan();
    }

    public bool GetDoldu()
    {
        return maddeAnim.doldu;
    }
    public int SetMaxMaddeSayisi()
    {
        return maddeAnim.maxMaddeSayisi;
    }

    public void KasayiDoldur() // bunu ca��r�p doluma ba�la!!
    {
        dolcakOlan = GetDolacakOlan();
        DisableAllPrefab();
        dolcakOlan.SetActive(true);

        int r = maddeAnim.maxMaddeSayisi; // bunu ne kadar dolacaksa o say� ile de�i�tirebiliriz.
        maddeAnim.GetItems(r);
    }

    GameObject GetDolacakOlan()
    {

        if (kasaMaddesi == KasaMaddesi.Wood)
        {
            maddeAnim = woodMA;
            return woodMaddePrefab;
        }
        if (kasaMaddesi == KasaMaddesi.Stone)
        {
            maddeAnim = stoneMA;
            return stoneMaddePrefab;
        }
        if (kasaMaddesi == KasaMaddesi.Iron)
        {
            maddeAnim = ironMA;
            return ironMaddePrefab;
        }

        return woodMaddePrefab;
    }

    void DisableAllPrefab()
    {

        woodMaddePrefab.SetActive(false);
        stoneMaddePrefab.SetActive(false);
        ironMaddePrefab.SetActive(false);
    }
}
