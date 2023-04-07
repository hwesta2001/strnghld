using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitInfoPanelControl : MonoBehaviour
{
    bool unitSelected;
    bool lastSelectionChange;
    [SerializeField] GameObject infoPanel;
    [SerializeField] TextMeshProUGUI infoText;
    string lastString;
    void Start()
    {
        infoPanel.SetActive(false);
    }

    void Update()
    {
        KontrolSelected();
        PanelKontrol();
    }

    void LateUpdate()
    {
        if (!unitSelected) return;
        if (lastString == CanGlobals.ins.SelectedUnit.info) return;
        lastString = CanGlobals.ins.SelectedUnit.info;
        infoText.text = lastString;
    }

    void PanelKontrol()
    {
        if (lastSelectionChange == unitSelected) return;
        lastSelectionChange = unitSelected;

        if (unitSelected)
        {
            infoPanel.SetActive(true);

        }
        else
        {
            infoPanel.SetActive(false);
            infoText.text = "";
        }
    }


    void KontrolSelected()
    {
        if (CanGlobals.ins.AUnitSeleted())
        {
            unitSelected = true;
        }
        else
        {
            unitSelected = false;
        }
    }
}
