using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CanGlobals : MonoBehaviour
{
    public Transform selectionArrow;
    ////public TextMeshProUGUI floatingText;
    //[SerializeField] FloatingText floatingText;


    public GameObject selectionPanel;
    public Image healtBar;
    [SerializeField] RectTransform rectTransform;

    public TextMeshProUGUI unitName;
    public Image icon;
    public Image nameBackground;

    public Unit SelectedUnit;
    Unit lastSelectedUnit;
    public Unit emptyUnit;

    public float globalArmorConstant = 0.06f;

    float lastHp;

    float barWight = 200;


    public static CanGlobals ins;

    void Awake()
    {
        ins = this;
    }

    void Start()
    {
        barWight = healtBar.transform.GetComponent<RectTransform>().sizeDelta.x;

        ClearSelection();
    }

    void Update()
    {
        SelectionControl();
        if (Input.GetKeyDown(KeyCode.T))
        {
            ClearSelection();
        }
    }

    public bool AUnitSeleted()
    {
        if (SelectedUnit == null || SelectedUnit == emptyUnit) return false;
        return true;
    }


    void SelectionControl()
    {
        if (SelectionChanged())
        {
            //if (SelectedUnit == null) return;
            //if (SelectedUnit == emptyUnit) return;
            if (!AUnitSeleted()) return;

            ChangeIcon(SelectedUnit.icon);
            ChangeName(SelectedUnit.unitName);
            if (SelectedUnit.team != 1) // 1inci Team her zaman player yani bu enemy demek!
            {
                nameBackground.color = Color.red;
            }
            else
            {
                nameBackground.color = Color.green;
            }
        }
    }

    void LateUpdate() // late updatete cünkü tüm hesaplamalar vs yapýldýktan sonra uiye gönderilsin!
    {
        HpBarSetter();
        SelectionArrowFallowTarget();
    }

    private void SelectionArrowFallowTarget()
    {
        if (!selectionArrow.gameObject.activeInHierarchy) return;
        if (SelectedUnit == null) return;
        if (SelectedUnit == emptyUnit) return;
        selectionArrow.position = SelectedUnit.selectionArrowTransform.position;
    }

    void HpBarSetter()
    {
        if (SelectedUnit == null) return;
        if (SelectedUnit == emptyUnit) return;

        if (SelectedUnit.hp != lastHp)
        {
            lastHp = SelectedUnit.hp;
            ChangeHp(SelectedUnit.hp, SelectedUnit.maxHp);
        }

    }

    bool SelectionChanged()
    {
        if (SelectedUnit == null) return false;
        if (SelectedUnit == emptyUnit) return false;
        if (SelectedUnit != lastSelectedUnit)
        {
            if (!selectionPanel.activeInHierarchy)
            {
                selectionPanel.SetActive(true);
            }
            lastSelectedUnit = SelectedUnit;
            return true;
        }
        return false;

    }



    void ChangeIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    void ChangeName(string _name)
    {
        unitName.text = _name;
    }

    void ChangeHp(float hp, float maxHp)
    {
        float oran = hp / maxHp;
        float barLenght = oran * barWight;
        rectTransform.sizeDelta = new Vector2(barLenght, rectTransform.sizeDelta.y);
        Color col = Color.green;
        if (oran < 0.5f && oran >= 0.3f)
        {
            col = Color.yellow;
        }
        if (oran < 0.3f)
        {
            col = Color.red;
        }
        healtBar.color = col;
    }

    public void SelectionArrowMove(Vector3 pos, float size)
    {
        selectionArrow.gameObject.SetActive(true);
        selectionArrow.transform.localScale = Vector3.one * size * .4f;
        //floatingText.gameObject.SetActive(true);
        //floatingText.DeActivateAll();
        selectionArrow.localPosition = pos;

    }


    public void SelectionArrowColor(Color col)
    {
        selectionArrow.GetComponentInChildren<Renderer>().material.color = col;
    }

    public void ClearSelection()
    {
        //floatingText.gameObject.SetActive(false);
        SelectedUnit = emptyUnit;
        lastSelectedUnit = emptyUnit;
        selectionPanel.SetActive(false);
        selectionArrow.gameObject.SetActive(false);
    }

    //public void FloatingText(string txt, Color col)
    //{
    //    TextMeshProUGUI tm = floatingText.GetFromPool();
    //    tm.text = txt;
    //    tm.color = col;
    //    tm.gameObject.SetActive(true);
    //    StartCoroutine(floatingText.DeActivate(tm));
    //}

}
