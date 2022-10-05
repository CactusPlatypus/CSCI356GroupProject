using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;
    // Use this for initialization
    void Start()
    {
        ReBuild();
        //BuildAll();
    }

    public void ReBuild()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }

    void BuildAll()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tilde))
        {
            ReBuild();
            //BuildAll();
        }
    }

}