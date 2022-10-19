using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation.Examples;

public class RoadSpawner : MonoBehaviour
{
    // To allow scripts to globally access the road spawner
    public static RoadSpawner instance;

    public ObstacleSpawner obstacleSpawner;

    // Store all road variations
    public GameObject[] roadPrefabs;

    // Store active roads in the scene, max of 8 for now
    private const int numRoads = 8;
    private Queue<GameObject> roads = new Queue<GameObject>(numRoads);

    // Initial road the player starts on
    public GameObject initialRoad;
  
    private GameObject SpawnRoad(Transform end)
    {
        // Make a random road prefab and add it to the queue
        GameObject randomRoad = roadPrefabs[Random.Range(0, roadPrefabs.Length)];
        GameObject road = Instantiate(randomRoad);
        roads.Enqueue(road);

        // Connect it to the end of the previous road
        Transform start = road.transform.Find("Start");
        road.transform.rotation = end.rotation * start.rotation;
        road.transform.position = end.position - start.position;

        // Add obstacles to road
        obstacleSpawner.SpawnObstacles(road);

        return road;
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void Start()
    {
        Transform lastEnd = transform;
        for (int i = 0; i < numRoads; i++)
        {
            GameObject road = SpawnRoad(lastEnd);
            lastEnd = road.transform.Find("End");
        }
    }

    public void TriggerEntered()
    {
        // Nuke the initial road if not already deleted
        if (initialRoad != null)
        {
            Destroy(initialRoad);
        }

        // Nuke the first road in queue
        Destroy(roads.Dequeue());

        // Attach new road to the end of the last
        SpawnRoad(roads.Last().transform.Find("End"));
    }
}
