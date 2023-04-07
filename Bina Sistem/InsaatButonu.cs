using UnityEngine;
using System.Collections;

public class InsaatButonu : MonoBehaviour
{

    [SerializeField] Transform player;

    [SerializeField]  // isperctorden ata!!!
    GameObject insaatAlani;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        insaatAlani = Globals.ins.insaatAlani;
        player = Globals.ins.PLAYER;
        if (insaatAlani == null)
        {
            Debug.Log("insaa alaný prefabýný scena atayýp inspectörde buraya yerleþtirin");
        }
        insaatAlani.SetActive(false);
    }

    public void InsaAlanCek() // butonda aktif
    {
        insaatAlani.SetActive(false);
        if (Globals.ins.nowBuilding)
        {
            PlayerInput.InsaaTButonTrigger = true;
            if (insaatAlani)
            {
                Globals.ins.nowBuilding = false;
                insaatAlani.SetActive(false);
            }
            return;
        }
        Vector3 point = Extantions.Onunde(player, Globals.ins.insaaUzakligi);
        Vector3 rot = Vector3.up * 90;

        insaatAlani.transform.SetPositionAndRotation(point, Quaternion.Euler(rot));
        insaatAlani.SetActive(true);
        //insaatAlani = Instantiate(insaAlanPrefab, point, Quaternion.Euler(rot));
    }


}
