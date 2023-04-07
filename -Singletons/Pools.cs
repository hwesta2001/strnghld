using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Pools
{
    public static List<IsciControl> Isciler = new();
    public static List<BuildingControl> Binalar = new();
    public static List<Transform> agaclar = new();
    public static List<Transform> isciSpawnPoints = new();


    public static List<Depo> depolar = new();
    public static List<Depo> BarnDepo = new();
    public static List<Depo> FoodDepo = new();
    public static List<Depo> Armory = new();


    public static List<Unit> AllUnits = new();
    public static List<Transform> InnRandomPoints = new();
    public static List<Transform> buildingKapilar = new();  // henuz kullanılmadı bir yerde

    public static void AddToPool<T>(this T obj, List<T> pool)
    {
        if (pool.Contains(obj)) return;
        pool.Add(obj);
    }
    public static void RemoveFromPool<T>(this T obj, List<T> pool)
    {
        if (!pool.Contains(obj)) return;
        pool.Remove(obj);
    }


    public static Transform EnYakinBul<T>(Transform trans, List<T> list) where T : Component
    {
        float dist1 = (trans.position - list[0].transform.position).sqrMagnitude;
        Transform donenTransform = list[0].transform;

        for (int i = 0; i < list.Count; i++)
        {
            float dist2 = (trans.position - list[i].transform.position).sqrMagnitude;
            if (dist2 < dist1)
            {
                donenTransform = list[i].transform;
                dist1 = dist2;
            }
        }

        return donenTransform;
    }


    public static Transform EnYakinDepoBul(Transform trans, List<Depo> list)
    {
        float dist1 = (trans.position - list[0].kapi.position).sqrMagnitude;
        Transform donenTransform = list[0].kapi;

        for (int i = 0; i < list.Count; i++)
        {
            float dist2 = (trans.position - list[i].kapi.position).sqrMagnitude;
            if (dist2 < dist1)
            {
                donenTransform = list[i].kapi;
                dist1 = dist2;
            }
        }

        return donenTransform;
    }


    public static void ClearAllPools()
    {
        Isciler.Clear();
        Binalar.Clear();
        agaclar.Clear();
        isciSpawnPoints.Clear();
        depolar.Clear();
        BarnDepo.Clear();
        FoodDepo.Clear();
        Armory.Clear();
        AllUnits.Clear();
        InnRandomPoints.Clear();
        buildingKapilar.Clear();
    }



}
