using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI popText, maxPopText;

    [SerializeField] TextMeshProUGUI goldtext, woodText, stoneText, ironText;

    [SerializeField] TextMeshProUGUI alertText00;
    [SerializeField] TextMeshProUGUI alertText01;
    [SerializeField] TextMeshProUGUI alertText02;

    IEnumerator coClearTextRoutine00;
    IEnumerator coClearTextRoutine01;
    IEnumerator coClearTextRoutine02;
    int tik = 0;
    public bool writtingNow;

    public static TextControl ins;

    void Awake()
    {
        ins = this;
        ClearText(alertText00);
        ClearText(alertText01);
        ClearText(alertText02);
    }

    void Start()
    {
        Globals.GuiRefresh += RefreshResurceUI;
    }


    void Update() //debug icin
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            //Color col = Color.HSVToRGB(0, 77f, 100f);
            Color col = Random.ColorHSV();
            AlertText("DenemE Texti ŞĞÇÜÜÜ+^^ 1234567890", 28, color: col, 2f);
        }

    }

    public void RefreshResurceUI()
    {
        goldtext.text = Globals.ins.Gold.ToString();
        woodText.text = Globals.ins.Wood.ToString();
        stoneText.text = Globals.ins.Stone.ToString();
        ironText.text = Globals.ins.Iron.ToString();
        popText.text = Globals.ins.Population.ToString();
        maxPopText.text = Globals.ins.MaxPopulation.ToString();
    }

    public void AlertText(string _text, int? fontSize30 = null, Color? color = null, float? gecikme = null)
    {
        writtingNow = true;
        TextMeshProUGUI writeTothis;

        if (alertText00.text == null)
        {
            writeTothis = alertText00;
            tik = 0;
        }
        else
        {
            if (alertText01.text == null)
            {
                writeTothis = alertText01;
                tik = 0;
            }
            else
            {
                if (alertText02.text == null)
                {
                    writeTothis = alertText02;
                    tik = 0;
                }
                else
                {
                    tik++;
                    if (tik == 1)
                    {
                        writeTothis = alertText00;
                    }
                    else if (tik == 2)
                    {
                        writeTothis = alertText01;
                    }
                    else
                    {
                        tik = 0;
                        writeTothis = alertText02;
                    }
                }
            }
        }

        int fontSizeClamped = Mathf.Clamp(fontSize30 ?? 30, 20, 30);
        BeginWriting(writeTothis, _text, fontSizeClamped, color ?? Color.white, gecikme ?? 3f);
        StartCoroutine(ColorFlash(writeTothis));
    }



    private void BeginWriting(TextMeshProUGUI alertText, string _text, int fontSize36, Color color, float gecikme)
    {

        alertText.text = _text;
        alertText.fontSize = fontSize36;
        alertText.color = color;

        if (alertText == alertText00)
        {
            if (coClearTextRoutine00 != null)
            {
                StopCoroutine(coClearTextRoutine00);
            }
            coClearTextRoutine00 = ClearAlertText(alertText, gecikme);
            StartCoroutine(coClearTextRoutine00);
        }
        else if (alertText == alertText01)
        {
            if (coClearTextRoutine01 != null)
            {
                StopCoroutine(coClearTextRoutine01);
            }
            coClearTextRoutine01 = ClearAlertText(alertText, gecikme);
            StartCoroutine(coClearTextRoutine01);
        }
        else
        {
            if (coClearTextRoutine02 != null)
            {
                StopCoroutine(coClearTextRoutine02);
            }
            coClearTextRoutine02 = ClearAlertText(alertText, gecikme);
            StartCoroutine(coClearTextRoutine02);
        }


    }

    IEnumerator ClearAlertText(TextMeshProUGUI alertText, float gecikme)
    {
        yield return new WaitForSeconds(gecikme);
        ClearText(alertText);
    }

    private void ClearText(TextMeshProUGUI alertText)
    {
        alertText.text = "";
        alertText.text = null;
        if (alertText00.text == null && alertText01.text == null && alertText02.text == null)
        {
            writtingNow = false;
            tik = 0;
        }
    }


    IEnumerator ColorFlash(TextMeshProUGUI alertText)
    {
        Color initColor = alertText.color;
        alertText.color = Color.black;
        yield return new WaitForSeconds(.14f);
        alertText.color = initColor;
    }
}