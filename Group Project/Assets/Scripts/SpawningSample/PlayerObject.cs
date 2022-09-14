using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    static PlayerObject instance;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("More than 1 player");
            Destroy(gameObject);
        }
        instance = this;
    }

    public static GameObject GetPlayerObject()
    {
        return instance.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
