using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    static PlayerObject instance;

    private void OnEnable()
    {
        Setup();
    }

    private void Awake()
    {
        Setup();
    }


    public void Setup()
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
        if(instance == null)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerObject>().Setup();
        }
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
