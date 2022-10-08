using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnDifferenceVector;
    [SerializeField] private Vector3 spawnStart;

    [SerializeField] private int minObstacleCount;
    [SerializeField] private int maxObstacleCount;
    private float totalPercent = 0;

    [SerializeField] private List<SpawnObject> spawnObjects;

    public void SpawnObstacles(GameObject road)
    {
        SetupActualPercent();
        int spawnCount = Random.Range(minObstacleCount, maxObstacleCount);
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnObject(road);
        }
    }

    private void SetupActualPercent()
    {
        if (spawnObjects.Count <= 0) return;

        totalPercent = 0;
        foreach (SpawnObject obj in spawnObjects)
        {
            totalPercent += obj.spawnPercent;
            obj.SetPercent(totalPercent);
        }
    }

    private GameObject PercentSpawn()
    {
        float randomNumber = Random.Range(0, totalPercent);
        foreach (SpawnObject obj in spawnObjects)
        {
            if (randomNumber < obj.GetPercent())
            {
                return obj.spawnObject;
            }
        }
        return null;
    }

    private void SpawnObject(GameObject road)
    {
        RoadMeshCreator mesh = road.GetComponentInChildren<RoadMeshCreator>();
        const float obstacleHeight = 1f;

        float location = Random.value;
        Vector3 pos = mesh.pathCreator.path.GetPointAtTime(location);
        Quaternion rot = mesh.pathCreator.path.GetRotation(location) * Quaternion.Euler(0f, 0f, 90f);

        float sidePos = Random.Range(-mesh.roadWidth, mesh.roadWidth);
        Vector3 sideOffset = rot * Vector3.right * sidePos;
        Vector3 topOffset = rot * Vector3.up * obstacleHeight;

        GameObject randomObstacle;

        if (spawnObjects.Count > 0)
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
