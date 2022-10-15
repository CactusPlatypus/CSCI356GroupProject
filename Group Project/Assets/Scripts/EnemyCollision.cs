using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private float timeSinceLastCollision = 3.0f;

    public void Update()
    {
        timeSinceLastCollision += Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy" && timeSinceLastCollision > 2.0f)
        {
            timeSinceLastCollision = 0.0f;
            
            gameObject.GetComponent<PlayerController>().addLives(-1);
            Debug.Log("GARYY NO: " + collision.transform.tag + " " + collision.transform.name);
        }
    }
}
