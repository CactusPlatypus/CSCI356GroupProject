using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private int livesDamage = 1;
    private float timeUntilHit = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && timeUntilHit - Time.time <= 0f)
        {
            timeUntilHit = Time.time + 2f;
            ScoreManager.instance.AddLives(-livesDamage);
        }
    }
}
