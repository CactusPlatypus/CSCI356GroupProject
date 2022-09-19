using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    // Store all road variations
    public GameObject[] roadPrefabs;

    // Store all obstacles
    public GameObject[] obstacles;

    // Store active roads in the scene, max of 16 for now
    private const int numRoads = 16;
    private Queue<GameObject> roads = new Queue<GameObject>(numRoads);

    // Initial road the player starts on to delete, remove later
    public GameObject initialRoad;

    // How separated objects should be when spawned on the road
    private const float obstacleSpacingPerRoad = 4f;

    private Transform player;

    private IEnumerator SpawnObstacles(GameObject road)
    {
        // Hack, wait for physics update before getting bounds
        yield return new WaitForSeconds(0.1f);

        Bounds bounds = road.GetComponentInChildren<MeshCollider>().bounds;

        float numObstacles = (bounds.size.x + bounds.size.z) / obstacleSpacingPerRoad;
        for (int i = 0; i < numObstacles; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(x, bounds.max.y, z), Vector3.down, out hit, bounds.size.y))
            {
                GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Length)];
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                Instantiate(randomObstacle, hit.point + Vector3.up * 0.5f, randomRotation, road.transform);
            }
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

        // Add obstacles to road using raycast
        //StartCoroutine(SpawnObstacles(road));

        return road;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        // Force initial road to be deleted, remove this later
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
