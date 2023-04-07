using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArea : MonoBehaviour
{
    [SerializeField]
    GameObject stonePrefab;
    [SerializeField]
    Transform area;
    [SerializeField]
    float ara = .6f;
    [SerializeField]
    List<Vector3> points = new List<Vector3>();
    float parentScaleKatsayiX;
    float parentScaleKatsayiZ;


    void Start()
    {
        if (area == null)
        {
            area = transform;
        }
        Destroy(area.GetComponent<MeshRenderer>());
        Destroy(area.GetComponent<MeshFilter>());
        area.position = new Vector3(area.position.x, 0, area.position.z);
        GridHesapla();

    }

    void GridHesapla()
    {
        parentScaleKatsayiX = area.localScale.x * area.parent.localScale.x;
        parentScaleKatsayiZ = area.localScale.z * area.parent.localScale.z;



        float x = area.position.x - parentScaleKatsayiX * .5f;
        float z = area.position.z - parentScaleKatsayiZ * .5f;
        Vector3 origin = new Vector3(x, 0, z);
        GetPoints(origin, parentScaleKatsayiX, parentScaleKatsayiZ, ara);

        for (int i = 0; i < points.Count; i++)
        {
            GameObject go = Instantiate(stonePrefab, points[i], Quaternion.Euler(0, Random.Range(0, 91), 0), area);
            go.transform.position = new Vector3(go.transform.position.x, 0, go.transform.position.z);
            //Random.Range(1, 1.4f)
            float sx = Random.Range(1, 1.4f) / parentScaleKatsayiX;
            float sy = Random.Range(.8f, 1.6f) / (area.transform.localScale.y * area.parent.localScale.y);
            float sz = Random.Range(1, 1.4f) / parentScaleKatsayiZ;
            go.transform.localScale = new Vector3(sx, sy, sz);
        }
    }


    void GetPoints(Vector3 _origin, float sizeX, float sizeZ, float aralik)
    {
        int x = (int)Mathf.Floor(sizeX / aralik);
        int z = (int)Mathf.Floor(sizeZ / aralik);

        float araX = _origin.x;
        float araZ = _origin.z;


        for (int i = 0; i < z + 1; i++)
        {
            for (int j = 0; j < x + 1; j++)
            {
                Vector3 _p = new Vector3(araX, 0, araZ);
                points.Add(_p);
                araX += aralik;
                if (araX > _origin.x + sizeX)
                {
                    araX = _origin.x;
                }
            }
            araZ += aralik;
            if (araZ > _origin.z + sizeZ)
            {
                araX = _origin.z;
            }
        }
    }

}
