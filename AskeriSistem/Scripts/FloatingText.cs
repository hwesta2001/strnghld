using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] List<TextMeshProUGUI> textPool = new List<TextMeshProUGUI>();



    void Start()
    {
        DeActivateAll();
    }


    void Update()
    {
        foreach (var item in textPool)
        {
            if (item.gameObject.activeInHierarchy)
            {
                item.transform.localPosition += new Vector3(Random.Range(-.6f, .6f) * Time.deltaTime, 1.2f * Time.deltaTime, 0);
            }
        }
    }
    void OnDisable()
    {
        DeActivateAll();
    }

    public IEnumerator DeActivate(TextMeshProUGUI go)
    {
        yield return new WaitForSeconds(1.3f);
        go.gameObject.SetActive(false);
        go.transform.localPosition = Vector3.zero;
    }


    public TextMeshProUGUI GetFromPool()
    {
        for (int i = 0; i < textPool.Count; i++)
        {
            if (!textPool[i].gameObject.activeInHierarchy)
            {
                return textPool[i];
            }
        }

        TextMeshProUGUI go = Instantiate(textPool[0].gameObject, transform).GetComponent<TextMeshProUGUI>();
        go.gameObject.SetActive(true);
        textPool.Add(go);
        return go;
    }


    public void DeActivateAll()
    {
        foreach (var item in textPool)
        {
            item.gameObject.SetActive(false);
            item.transform.localPosition = Vector3.zero;
        }
    }
}
