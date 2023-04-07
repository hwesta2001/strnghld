using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSystem : MonoBehaviour
{
    public GameObject silinecekObje;
    [SerializeField] LayerMask deletedObjectLayers;
    private Camera cam;
    bool deletingNow;
    [SerializeField] Image deleteCursor;
    private void Start()
    {
        cam = Globals.ins.mainCamera;
    }
    private void Update()
    {
        if (!deletingNow) return;
        ObjeKontrol();
    }

    private void ObjeKontrol()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, deletedObjectLayers))
        {
            deleteCursor.color = Color.red;
            deleteCursor.rectTransform.localScale = Vector3.one * 1.5f;
        }
        else
        {
            deleteCursor.color = Color.white;
            deleteCursor.rectTransform.localScale = Vector3.one;
        }
    }

    public void DeletingStart()
    {
        deletingNow = true;
    }

    public void DeletingEnd()
    {
        deletingNow = false;
    }

    public void DeleteThis()
    {
        RayReturn();

        if (silinecekObje == null)
        {
            TextControl.ins.AlertText("Select an object to delete M'Lord!", 38, Color.cyan, 2f);
            return;
        }
        else
        {
            Destroy(silinecekObje);
        }
    }

    void RayReturn()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, deletedObjectLayers))
        {
            silinecekObje = hit.transform.gameObject;
        }
        else
        {
            silinecekObje = null;
        }
    }
}
