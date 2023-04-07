using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject BuildCanvas;
    [SerializeField] GameObject DeleteCanvas;

    [SerializeField] List<GameObject> allPanels = new List<GameObject>();
    public enum Panel
    {
        Main, // 0 ýncý enum 
        Build, // 1 inci enum vs vs gidebilir.
        Delete, // 2 inci
    }
    private Panel panel;

    void Start()
    {
        allPanels.Add(MainCanvas);
        allPanels.Add(BuildCanvas);
        allPanels.Add(DeleteCanvas);
        PanelDegistir(0);
    }

    public void PanelDegistir(int p)
    {
        panel = (Panel)p;
        switch (panel)
        {
            case Panel.Main:
                ActivtePanel(MainCanvas);
                break;

            case Panel.Build:
                ActivtePanel(BuildCanvas);
                break;

            case Panel.Delete:
                ActivtePanel(DeleteCanvas);
                break;
        }
    }



    void ActivtePanel(GameObject go)
    {
        foreach (var item in allPanels)
        {
            item.SetActive(false);
        }
        go.SetActive(true);
    }


    //void BuildActive()
    //{
    //    foreach (var item in allPanels)
    //    {
    //        item.SetActive(false);
    //    }
    //    BuildPanel.SetActive(true);
    //}
}
