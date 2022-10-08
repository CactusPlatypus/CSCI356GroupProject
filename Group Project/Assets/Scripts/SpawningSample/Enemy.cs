using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * movementSpeed);
    }
}