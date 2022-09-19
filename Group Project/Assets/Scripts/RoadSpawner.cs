using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PathCreation.Examples;

public class RoadSpawner : MonoBehaviour
{
    // To allow scripts to globally access the road spawner
    public static RoadSpawner instance;

    // Store all road variations
    public GameObject[] roadPrefabs;

    // Store all obstacles
    public GameObject[] obstacles;

    // Store active roads in the scene, max of 16 for now
    private const int numRoads = 16;
    private Queue<GameObject> roads = new Queue<GameObject>(numRoads);

    // Initial road the player starts on, maybe improve later
    public GameObject initialRoad;

    private const int obstaclesPerRoad = 8;
    private const float obstacleHeight = 5f;

    private void SpawnObstacles(GameObject road)
    {
        RoadMeshCreator mesh = road.GetComponentInChildren<RoadMeshCreator>();

        for (int i = 0; i < obstaclesPerRoad; i++)
        {
            float location = Random.value;
            Vector3 pos = mesh.pathCreator.path.GetPointAtTime(location);
            Quaternion rot = mesh.pathCreator.path.GetRotation(location) * Quaternion.Euler(0f, 0f, 90f);

            float sidePos = Random.Range(-mesh.roadWidth, mesh.roadWidth);
            Vector3 sideOffset = rot * Vector3.right * sidePos;
            Vector3 topOffset = rot * Vector3.up * obstacleHeight;

            GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Length)];
            Instantiate(randomObstacle, pos + sideOffset + topOffset, rot, road.transform);
        }
    }
    
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
        //SpawnObstacles(road);

        return road;
    }

    private void Start()
    {
        instance = this;

        // Force initial road to be deleted, maybe improve this later
        roads.Enqueue(initialRoad);

        Transform lastEnd = transform;
        for (int i = 0; i < numRoads; i++)
        {
            GameObject road = SpawnRoad(lastEnd);
            lastEnd = road.transform.Find("End");
        }
    }

    public void TriggerEntered()
    {
        // Nuke the first road in queue
        Destroy(roads.Dequeue());

        // Attach new road to the end of the last
        SpawnRoad(roads.Last().transform.Find("End"));
    }
}
