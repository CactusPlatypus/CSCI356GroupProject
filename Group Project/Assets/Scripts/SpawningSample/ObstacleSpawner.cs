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


    float totalPercent = 0;
    int percentNumber = 1000;
    [SerializeField]List<SpawnObject> spawnObjects;
    [SerializeField] List<GameObject> obstaclePrefabs = new List<GameObject>();
    [SerializeField]List<GameObject> spawnedObjects = new List<GameObject>();//maybe want to be a list<List<GameObject>> as it means it can keep track of all the different pieces that have obstacles/enemies on them and it can be like a queue
    [SerializeField]List<List<GameObject>> spawnedObjectsList = new List<List<GameObject>>();


    [SerializeField] GameObject obstacleParent;//this can maybe get changed to be the parent piece which means that it will get cleared/destroyed when piece gets removed/changed


    //get the rect that the obstacles can spawn in.
    //pick random locations in that area to spawn obstacles
    //pick random number of obstacles to spawn



    public void SpawnObstacles(GameObject roadPrefab)
    {
        Debug.Log("Spawned");
        Debug.Log("New road = " + roadPrefab.name);
        SetupActualPercent();
        spawnedObjects.Clear();
        int spawnCount = Random.Range(minObstacleCount, maxObstacleCount);

        GameObject newGameobjectParent = new GameObject("obstacleParent");
        newGameobjectParent.transform.parent = roadPrefab.transform;


        PathCreation.PathCreator path = roadPrefab.GetComponentInChildren<PathCreation.PathCreator>();
        roadWidth = roadPrefab.GetComponentInChildren<PathCreation.Examples.RoadMeshCreator>().roadWidth;
        roadLength = path.path.length;


        for (int i = 0; i < spawnCount; i++)
        {
            float randomX = Random.Range(-roadWidth, roadWidth);//roadPosition
            float randomZ = Random.Range(0, roadLength);//distanceTravelled

            
            //Vector3 spawnLocation = spawnStart + new Vector3(randomX, 0, randomZ);
            Vector3 spawnLocation = GetSpawnLocation(randomZ, randomX, path);
            Quaternion rot = path.path.GetRotationAtDistance(randomZ) * Quaternion.Euler(0f, 0f, 90f);


            //SpawnObject(spawnLocation,rot, newGameobjectParent.transform);
            SpawnObject(roadPrefab);
        }
        Debug.Log("Pre Clear");
        //PrintLists();
        spawnedObjectsList.Add(spawnedObjects);
        spawnedObjects = new List<GameObject>();
        Debug.Log("Post Clear");
        //PrintLists();
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
            totalPercent = spawnObjects[i].spawnPercent * percentNumber;
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



    void DeleteAndRemoveFirstList()
    {
        if(spawnedObjectsList.Count < 1)
        {
            return;
        }
        List<GameObject> currentList = spawnedObjectsList[0];

        spawnedObjectsList.RemoveAt(0);

        foreach(GameObject obj in currentList)
        {
            Destroy(obj);
        }
        currentList.Clear();
    }

    void PrintLists()
    {
        if(spawnedObjects != null)
        {
            
            Debug.Log("spawnedObject count: " + spawnedObjects.Count);
            for(int i = 0; i < spawnedObjects.Count; i++)
            {
                Debug.Log(spawnedObjects[i].name);
            }
        }
        else
        {
            Debug.Log("spawnedObjects = null");
        }

        
        if(spawnedObjectsList != null)
        {

            Debug.Log("spawnedObjectList count: " + spawnedObjectsList.Count);
            for (int i = 0; i < spawnedObjectsList.Count; i++)
            {
                Debug.Log("spawnedObject count: " + spawnedObjectsList[i].Count);
                for (int j = 0; j < spawnedObjectsList[i].Count; j++)
                {
                    Debug.Log(spawnedObjectsList[i][j].name);
                }
            }
        }
        else
        {
            Debug.Log("spawnedObjectsList = null");
        }

        Debug.Log("");
        Debug.Log("");


    }

    Vector3 GetSpawnLocation(float distanceTravelled, float roadPosition)
    {
        Vector3 spawnLocation = new Vector3();

        //depends on implementation


        //for straight road
        spawnLocation = spawnStart + new Vector3(roadPosition, 0, distanceTravelled);

        //for spline
       
        /*
        float splineZ = getSplineZ(distanceTravelled);
        float splineX = GetSplineX(distanceTravelled, roadPosition);
        spawnLocation = spawnStart + new Vector3(splineZ, 0, splineX);
         */


        return spawnLocation;
    }

    Vector3 GetSpawnLocation(float distanceTravelled, float roadPosition, PathCreation.PathCreator path)
    {
        Vector3 spawnLocation = new Vector3();

        //depends on implementation


        //for straight road
        //spawnLocation = spawnStart + new Vector3(roadPosition, 0, distanceTravelled);

        //for spline

        
        Vector3 splineZ = path.path.GetPointAtDistance(distanceTravelled);
        Quaternion splineX = path.path.GetRotationAtDistance(distanceTravelled) * Quaternion.Euler(0f, 0f, 90f);

        Vector3 sideOffset = splineX * Vector3.right * distanceTravelled;
        Vector3 topOffset = splineX * Vector3.up * 1;

        Vector3 sideways = splineZ + sideOffset + topOffset;

        //spawnLocation = spawnStart + new Vector3(splineZ, 0, splineX);



        return sideways;
    }

    void SpawnObject(Vector3 location, Quaternion rot, Transform parentTransform)
    {
        GameObject newObject = null;
        if (obstaclePrefabs.Count > 0)
        {

            int randomObstacle = Random.Range(0, obstaclePrefabs.Count);
            newObject = Instantiate(obstaclePrefabs[randomObstacle], parentTransform);
            //newObject = Instantiate(PercentSpawn());

            
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

        GameObject randomObstacle = null;

        if (obstaclePrefabs.Count > 0)
        {
            randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Count)];
        }
        else
        {
            randomObstacle = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        Instantiate(randomObstacle, pos + sideOffset + topOffset, rot, road.transform);
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
        //SpawnObstacles();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)){
            //SpawnObstacles();
        }
        if (Input.GetKeyDown(KeyCode.P)){
            //RemoveObjects();
            DeleteAndRemoveFirstList();
        }
    }
}
