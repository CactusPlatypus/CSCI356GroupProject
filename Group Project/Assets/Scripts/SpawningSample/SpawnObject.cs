using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject
{
    public GameObject spawnObject;
    public float spawnPercent; // Value we put in
    private float realSpawnPercent; // Value needed to spawn, random must be below this

    public void SetPercent(float percent)
    {
        realSpawnPercent = percent;
    }

    public float GetPercent()
    {
        return realSpawnPercent;
    }
}