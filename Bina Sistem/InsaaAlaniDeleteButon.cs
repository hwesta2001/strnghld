using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsaaAlaniDeleteButon : MonoBehaviour
{
    public void DeleteInsaaAlani() // buttonla cal�s�yor
    {
        //Destroy(gameObject, .1f);
        StartCoroutine(DeactiveThis());
    }

    IEnumerator DeactiveThis()
    {
        yield return new WaitForSeconds(.001f);
        gameObject.SetActive(false);
    }
}
