using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharController : MonoBehaviour
{
    public RoadSpawner spawnManager;

    private Rigidbody rb;
    private const float horizontalSpeed = 70f;
    private const float verticalSpeed = 60f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * horizontalSpeed;
        //float vMovement = Input.GetAxis("Vertical") * verticalSpeed;

        rb.velocity = transform.forward * verticalSpeed - transform.up * 20f;
        transform.eulerAngles += transform.up * hMovement * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.TriggerEntered();
        }
    }
}
