using UnityEngine;

public class TekerHizAnimKontrol : MonoBehaviour
{
    [SerializeField] Transform teker;
    [SerializeField] Transform kasa;
    public float speed = 0;
    Vector3 lastPosition = Vector3.zero;


    void Update()
    {
        if (speed > 0)
        {
            TekerDondur();
            KasaDondur(-94, 20);
        }
        else
        {
            KasaDondur(-82, 10);
        }

    }

    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;
    }

    void TekerDondur()
    {
        teker.Rotate(Vector3.up, 45 * Time.deltaTime * -180 * speed);
    }
    void KasaDondur(float rot, float speed)
    {
        Quaternion newRot = Quaternion.Euler(rot, 0, 90);
        kasa.localRotation = Quaternion.Lerp(kasa.localRotation, newRot, Time.deltaTime * speed);
    }
}
