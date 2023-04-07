using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonsControl : MonoBehaviour
{
    // serialize cünkü inspectorden de ayarlayabiliriz optimizayson icin
    // böylece starttaki AddAllButtonsToList calýþtýrýlmasa da olur.
    [SerializeField] Transform ilkButon;


    [SerializeField] List<RectTransform> allBuildButons = new List<RectTransform>(); // inspectorden elle ekle!!!
    [SerializeField] List<Vector3> points = new List<Vector3>();
    [SerializeField] List<bool> actives = new List<bool>();
    bool activeLoop;



    void Start()
    {
        AddAllButtonsToList();
        activeLoop = true;
        Globals.WALL_KURMA.StartWallCreate += ActiveHaleGeriDon;
    }


    void AddAllButtonsToList()
    {
        points.Clear();
        for (int i = 0; i < allBuildButons.Count; i++)
        {
            points.Add(allBuildButons[i].localPosition);
        }

        actives.Clear();
        for (int i = 0; i < allBuildButons.Count; i++)
        {
            actives.Add(allBuildButons[i].gameObject.activeInHierarchy);
        }

    }

    public void DeactivateAll(GameObject go)
    {
        if (activeLoop)
        {
            activeLoop = false;
            actives.Clear();
            for (int i = 0; i < allBuildButons.Count; i++)
            {
                actives.Add(allBuildButons[i].gameObject.activeInHierarchy);
            }

            foreach (RectTransform item in allBuildButons)
            {

                item.gameObject.SetActive(false);
            }

            go.SetActive(true);
            go.transform.position = ilkButon.position;
        }
        else
        {
            activeLoop = true;
            for (int i = 0; i < allBuildButons.Count; i++)
            {
                allBuildButons[i].localPosition = points[i];
                allBuildButons[i].gameObject.SetActive(actives[i]);
            }
        }
    }

    public void ActiveHaleGeriDon()
    {

        if (!activeLoop)
        {
            for (int i = 0; i < allBuildButons.Count; i++)
            {
                allBuildButons[i].localPosition = points[i];
                allBuildButons[i].gameObject.SetActive(actives[i]);
            }
            activeLoop = true;
        }


    }

}
