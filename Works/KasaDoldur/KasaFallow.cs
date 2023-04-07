using UnityEngine;

public class KasaFallow : MonoBehaviour
{
    [SerializeField] float speed = 100;
    [SerializeField] Transform trans;

    bool fallowing = true;


    public void KasaTarget(Transform tr, bool fallow)
    {
        trans = tr;
        fallowing = fallow;
    }

    void Update()
    {
        if (!trans) return;
        if (!fallowing) return;
        FallowObje(trans);
        LookAtObje(trans);
    }

    void LookAtObje(Transform trans)
    {
        Quaternion rot = Quaternion.Lerp(transform.rotation, trans.rotation, Time.deltaTime * speed * .051f);
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        transform.rotation = rot;
    }

    void FallowObje(Transform trans)
    {
        Vector3 move = trans.position;
        transform.position = move;
    }
}
