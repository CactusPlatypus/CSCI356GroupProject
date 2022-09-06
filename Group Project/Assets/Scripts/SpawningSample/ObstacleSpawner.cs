using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    Vector3 playerLocation;

    [SerializeField]Vector3 spawnStart;

    [SerializeField]float roadWidth;//from middle to side
    [SerializeField] float roadLength;

    [SerializeField] int maxObstacleCount;
    [SerializeField]int minObstacleCount;

    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();
    List<GameObject> spawnedObjects = new List<GameObject>();

    [SerializeField] GameObject obstacleParent;


    //get the rect that the obstacles can spawn in.
    //pick random locations in that area to spawn obstacles
    //pick random number of obstacles to spawn



    void SpawnObstacles()
    {
        int spawnCount = Random.Range(minObstacleCount, maxObstacleCount);

        for (int i = 0; i < spawnCount; i++)
        {
            float randomX = Random.Range(-roadWidth, roadWidth);
            float randomZ = Random.Range(0, roadLength);

            Vector3 spawnLocation = spawnStart + new Vector3(randomX, 0, randomZ);
            SpawnObject(spawnLocation);
        }
    }


    void SpawnObject(Vector3 location)
    {
        GameObject newObject = null;
        if (obstaclePrefabs.Count > 0)
        {
            int randomObstacle = Random.Range(0, obstaclePrefabs.Count);
            newObject = Instantiate(obstaclePrefabs[randomObstacle]);
        }
        else
        {
            newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        newObject.transform.position = location;
        spawnedObjects.Add(newObject);
        if (obstacleParent != null)
            newObject.transform.parent = obstacleParent.transform;
    }

    void RemoveObjects()
    {
        for(int i = 0;i < spawnedObjects.Count; i++)
        {
            if(spawnedObjects[i] != null)
                Destroy(spawnedObjects[i]);
        }
        spawnedObjects.Clear();
    }


    // Start is called before the first frame update
    void Start()
    {
        SpawnObstacles();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)){
            SpawnObstacles();
        }
        if (Input.GetKeyDown(KeyCode.P)){
            RemoveObjects();
        }
    }
}
