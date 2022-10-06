using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] Vector3 spawnDifferenceVector;

    [SerializeField]Vector3 spawnStart;

    [SerializeField] int maxObstacleCount;
    [SerializeField]int minObstacleCount;


    float totalPercent = 0;
    int percentNumber = 1000;
    [SerializeField]List<SpawnObject> spawnObjects;
    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();

    [SerializeField] GameObject obstacleParent;//this can maybe get changed to be the parent piece which means that it will get cleared/destroyed when piece gets removed/changed




    public void SpawnObstacles(GameObject roadPrefab)
    {
        SetupActualPercent();
        int spawnCount = Random.Range(minObstacleCount, maxObstacleCount);

        GameObject newGameobjectParent = new GameObject("obstacleParent");
        newGameobjectParent.transform.parent = roadPrefab.transform;

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnObject(roadPrefab);
        }
    }


    void SetupActualPercent()
    {
        if(spawnObjects.Count <= 0)
        {
            return;
        }
        //could do math to set percentNumber to make sure everything is at least 1
        totalPercent = 0;
        for (int i = 0;i < spawnObjects.Count;i++)
        {
            totalPercent += spawnObjects[i].spawnPercent * percentNumber;
            spawnObjects[i].realSpawnPercent = totalPercent;
        }


    }


    GameObject PercentSpawn()
    {

        float randomNumber = Random.Range(0, totalPercent);
        int pickedIndex = 0;
        for (int i = 0; i < spawnObjects.Count; i++)
        {
            if (randomNumber < spawnObjects[i].realSpawnPercent)
            {
                pickedIndex = i;
                break;
            }
        }

        GameObject objectToSpawn = spawnObjects[pickedIndex].spawnObject;
        return objectToSpawn;

    }

    void SpawnObject(GameObject road)
    {
        PathCreation.Examples.RoadMeshCreator mesh = road.GetComponentInChildren<PathCreation.Examples.RoadMeshCreator>();
        float obstacleHeight = 1;

        float location = Random.value;
        Vector3 pos = mesh.pathCreator.path.GetPointAtTime(location);
        Quaternion rot = mesh.pathCreator.path.GetRotation(location) * Quaternion.Euler(0f, 0f, 90f);

        float sidePos = Random.Range(-mesh.roadWidth, mesh.roadWidth);
        Vector3 sideOffset = rot * Vector3.right * sidePos;
        Vector3 topOffset = rot * Vector3.up * obstacleHeight;

        GameObject randomObstacle;

        if (obstaclePrefabs.Count > 0)
        {
            randomObstacle = PercentSpawn();
        }
        else
        {
            randomObstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        Instantiate(randomObstacle, pos + sideOffset + topOffset + spawnDifferenceVector, rot, road.transform);
    }

}
