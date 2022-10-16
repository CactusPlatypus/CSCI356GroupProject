using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private float timeSinceLastCollision = 3f;

    public void Update()
    {
        timeSinceLastCollision += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && timeSinceLastCollision > 2f)
        {
            timeSinceLastCollision = 0f;
            ScoreManager.instance.AddLives(-1);
        }
    }
}
