using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]float movementSpeed = 10;
    bool removeAfterPass = true;
    Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = PlayerObject.GetPlayerObject().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Move();
        }
    }


    void Move()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * movementSpeed);
    }

}
