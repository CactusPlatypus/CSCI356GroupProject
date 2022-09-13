using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    // Store all road variations
    public GameObject[] roadPrefabs;

    // Store active roads in the scene, max of 64 for now
    private const int numRoads = 64;
    private Queue<GameObject> roads = new Queue<GameObject>(numRoads);

    private Transform player;
    
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

        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

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
