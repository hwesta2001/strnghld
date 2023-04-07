using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{ 
    public enum Name
    {
        Gold,
        Wood,
        Stone,
        Iron,
    };
    public Name name;
    public int neKadar;

}
