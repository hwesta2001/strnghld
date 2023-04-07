using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{

    public struct DebugVerisi
    {
        public string name;
        public float deger;
    }

    [SerializeField] GameObject DebugPanelObject;
    [SerializeField] TextMeshProUGUI textMesh;

    private List<TextMeshProUGUI> texts = new();
    private List<DebugVerisi> veriler = new();

    private int controlIdleIsci;

    void OnEnable()
    {
        //Globals.GuiRefresh += UpdateDebugPanel;
    }

    void OnDisable()
    {
        Globals.GuiRefresh -= UpdateDebugPanel;
    }



    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.001f);
        Globals.GuiRefresh += UpdateDebugPanel;
        controlIdleIsci = 0;
        textMesh.gameObject.SetActive(false);
        SetVeriler();

        for (int i = 0; i < veriler.Count; i++)
        {
            GameObject go = Instantiate(textMesh.gameObject, DebugPanelObject.transform);
            go.SetActive(true);
            texts.Add(go.GetComponent<TextMeshProUGUI>());
        }
    }
    private void Update()
    {
        IdleIsciControl(); // baya kasar bu dikkat, build öncesi cýkarmayý unutma!!!

    }


    private void SetVeriler() // tüm debug verilerini asaðý ekle.
    {
        veriler.Clear();
        veriler.Add(CreateVeri("wheat", (float)Globals.ins.Weath));
        veriler.Add(CreateVeri("flour", (float)Globals.ins.Flour));
        veriler.Add(CreateVeri("idle isci", (float)PoolGet()));
    }



    DebugVerisi CreateVeri(string _name, float _deger)
    {
        DebugVerisi veri;
        veri.name = _name;
        veri.deger = _deger;
        return veri;
    }


    void UpdateDebugPanel()
    {
        //Debug.Log("updating DebugPanel");

        SetVeriler();

        for (int i = 0; i < veriler.Count; i++)
        {
            texts[i].text = veriler[i].name + " : " + veriler[i].deger;
        }

    }


    private void IdleIsciControl()
    {
        if (controlIdleIsci == PoolGet()) return;
        controlIdleIsci = PoolGet();
        UpdateDebugPanel();
    }

    int PoolGet()
    {
        int a = 0;
        if (Pools.Isciler.Count > 0)
        {

            for (int i = 0; i < Pools.Isciler.Count; i++)
            {
                if (Pools.Isciler[i].isci.gorev == IsciGorevi.Idle)
                {
                    a++;
                }
            }
        }
        return a;
    }

}
