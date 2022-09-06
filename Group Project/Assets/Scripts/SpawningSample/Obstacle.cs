using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] float health;


    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if(collision.transform.GetComponent<SamplePlayerMovement>() != null)
            {
                OnHit();
            }
        }
    }

    void OnHit()
    {
        //Debug.Log("Has been hit");
        DestroyObstacle();
    }

    void DestroyObstacle()
    {
        if (gameObject != null) Destroy(gameObject);
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
