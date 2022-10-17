﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    //private Animator characterAnimator;
    private Transform player;
    private Transform characterTransform;
    [SerializeField] private GameObject ragdoll;

    private bool usedLeap = false;

    void Start()
    {
        //characterAnimator = GetComponentsInChildren<Animator>()[0];
        player = GameObject.FindWithTag("Player").transform;
        characterTransform = transform.Find("Kaleb").transform;
    }

    void Update()
    {
        if (usedLeap) return;
        if (Vector3.Distance(transform.position, player.position) < 25f)
        {
            gameObject.GetComponent<EnemyMovement>().currentState = EnemyState.Leaping;
            Leap();
        }
        else
        {
            //Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z -0.791f);
            characterTransform.position = transform.position;
        }
    }

    private void Leap()
    {
        usedLeap = true;

        //disable animator
        GameObject spawnedRagdoll = Instantiate(ragdoll);
        spawnedRagdoll.transform.position = transform.position;
        spawnedRagdoll.transform.LookAt(player);

        Destroy(gameObject);
    }
}
