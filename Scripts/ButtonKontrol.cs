using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonKontrol : MonoBehaviour
{
    [SerializeField]  // inspectorden ekle
    List<GameObject> childButtons = new();

    private void OnEnable()
    {
        if (childButtons.Count <= 0)
        {
            Debug.LogWarning("childButtonlarý eklemedin");
        }

        AktiveEt(childButtons[0]);
    }

    public void DeactiveAll()
    {
        foreach (var item in childButtons)
        {
            item.SetActive(false);
        }
    }

    public void AktiveEt(GameObject _button)
    {
        StartCoroutine(DelayAbit(_button));
    }

    IEnumerator DelayAbit(GameObject _button)
    {
        yield return new WaitForEndOfFrame();
        DeactiveAll();
        _button.SetActive(true);
    }

}
