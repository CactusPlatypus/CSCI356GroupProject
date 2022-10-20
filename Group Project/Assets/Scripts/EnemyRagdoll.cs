using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    private Transform player;
    private Transform characterTransform;
    [SerializeField] private GameObject ragdoll;

    private bool usedLeap = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        characterTransform = transform.Find("Kaleb").transform;
    }

    void Update()
    {
        if (usedLeap) return;
        if (Vector3.Distance(transform.position, player.position) < 25f)
        {
            Leap();
        }
        else
        {
            characterTransform.position = transform.position;
        }
    }

    private void Leap()
    {
        gameObject.GetComponent<EnemyMovement>().currentState = EnemyState.Leaping;
        usedLeap = true;

        // Disable animator
        GameObject spawnedRagdoll = Instantiate(ragdoll);
        spawnedRagdoll.transform.position = transform.position;
        spawnedRagdoll.transform.LookAt(player);

        Destroy(gameObject);
    }
}
