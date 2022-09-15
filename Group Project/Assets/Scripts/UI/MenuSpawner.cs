using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private float cooldown = 5;
    private float currentTime = 0.0f;
  
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > cooldown)
        {
            currentTime = 0;
            GameObject spawned = Instantiate(spawnedObject, transform);
       
        }
    }
}
