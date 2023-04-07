using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerdivenManager : MonoBehaviour
{
    public GameObject merdivenPrefab;
    public bool canUpdate;

    [SerializeField]
    LayerMask wallLayer;
    [SerializeField]
    //Transform mManagerParent;

    Camera cam;
    MeshRenderer meshRenderer;
    bool nowColliding;

    [SerializeField] // gecici
    Transform merdiven;
    [SerializeField] // gecici
    float insaaUzaklýgi = 5.6f;
    [SerializeField] // gecici
    bool duvaraCarpti;
    [SerializeField] // gecici
    Transform wall = null;
    [SerializeField] // gecici
    Quaternion lastRotation;
    Transform colliderObject;


    #region Buton Metotolarý Instantiate ve Cancel

    public void InstantiateMerdiven()
    {
        canUpdate = true;
        colliderObject.gameObject.SetActive(true);
        if (merdiven == null)
        {
            merdiven = Instantiate(merdivenPrefab).transform;
        }
        else
        {
            Destroy(merdiven.gameObject);
            merdiven = Instantiate(merdivenPrefab).transform;
        }
        meshRenderer = merdiven.Find("gfx").GetComponent<MeshRenderer>();
    }

    public void CancelMerdiven() // bir butonla merdiven kurma cancel olacak.
    {

        colliderObject.gameObject.SetActive(false);
        canUpdate = false;
        if (merdiven == null) return;
        Destroy(merdiven.gameObject);
        merdiven = null;
    }

    #endregion


    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.02f);
        colliderObject = transform.Find("boxCollider");
        cam = Globals.ins.mainCamera;
        insaaUzaklýgi = Globals.ins.insaaUzakligi;
        transform.parent = cam.transform;
        //transform.localPosition = Vector3.zero;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        colliderObject.localScale = new(colliderObject.localScale.x, colliderObject.localScale.y, insaaUzaklýgi * .5f);
        colliderObject.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (!canUpdate) return;
        RendererColorChange();
        if (duvaraCarpti)
        {
            if (wall == null) return;
            RotateToWall(merdiven, wall);
            if (Input.GetMouseButtonDown(0))
            {
                if (Side.merdivenColliding)
                {
                    Debug.Log("Merdiven Colliding");
                    return;
                }
                canUpdate = false;
                merdiven.localScale *= 0.98f;
                meshRenderer.material.color = Color.white;
                merdiven = null;
                Side.MerdivenKuruldu?.Invoke();
            }
        }
        else
        {
            if (merdiven == null) return;
            GridMove(merdiven);
            Quaternion rot = Quaternion.LookRotation(-cam.transform.forward, Vector3.up);
            rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, rot.eulerAngles.z);
            merdiven.localRotation = rot;
        }
    }

    Color col = Color.white;
    void RendererColorChange()
    {
        if (merdiven == null) return;

        if (duvaraCarpti)
        {
            col = Side.merdivenColliding ? Color.red : Color.green;
        }
        else
        {
            col = Side.merdivenColliding ? Color.red : Color.white;
        }

        meshRenderer.material.color = col;

        nowColliding = Side.merdivenColliding;

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            duvaraCarpti = true;
            wall = other.transform;
            Debug.Log("duvara carpiyor");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            duvaraCarpti = false;
            wall = null;
            Debug.Log("duvara carpma bitti");
        }
    }

    void RotateToWall(Transform tr, Transform _wall)
    {
        Vector3 pos = _wall.position;
        pos.y = 0;
        tr.position = pos;
        if (cam.CameraRayControl(out RaycastHit hit, insaaUzaklýgi * 2f, wallLayer))
        {
            lastRotation.eulerAngles = SideAngelDetermine(hit.point);
        }
        tr.localRotation = lastRotation;
    }

    private void GridMove(Transform tr)
    {
        // tr.position = Onunde(cam.transform, insaaUzaklýgi);
        tr.position = Extantions.Onunde(cam.transform, insaaUzaklýgi);
    }


    // Extentionda var asagidaki metot: 
    // G
    Vector3 Onunde(Transform target, float? ara = null) // ara kadar targetin önüne yere yerleþtirir.
    {
        float a = ara ?? 12f;
        Vector3 forwardPoint = target.position + target.forward * a;
        forwardPoint.y = 0;
        return forwardPoint;
    }



    Vector3 SideAngelDetermine(Vector3 point)
    {

        Sides side = Side.Detect(point, wall);

        if (side == Sides.Left)
        {
            Debug.Log("left");
            return wall.eulerAngles + Vector3.up * 270;
        }
        else if (side == Sides.Right)
        {
            Debug.Log("right");
            return wall.eulerAngles + Vector3.up * 90;
        }
        else if (side == Sides.Back)
        {
            Debug.Log("back");
            return wall.eulerAngles + Vector3.up * 180;
        }
        else
        {
            Debug.Log("front");
            return wall.eulerAngles + Vector3.up * 0;
        }

    }
}