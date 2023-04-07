using UnityEngine;
using UnityEngine.UI;

//[System.Serializable]
[CreateAssetMenu(fileName = "BinaName", menuName = "Bina", order = 2)]
public class Bina : ScriptableObject
{
    public string Name;
    public GameObject prefab;
    public Sprite icon;
    public int isciSayisi;
    public float buildTime = 2f;
    public Resource[] resources;
}
