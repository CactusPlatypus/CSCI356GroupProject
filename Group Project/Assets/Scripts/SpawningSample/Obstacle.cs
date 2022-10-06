using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null)
        {
            if(collision.transform.CompareTag("Player"))
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


}
