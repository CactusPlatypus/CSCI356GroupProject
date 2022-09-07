using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharController : MonoBehaviour
{
    public SpawnManager spawnManager;

    private Rigidbody rb;
    private float horizontalSpeed = 20f;
    private float verticalSpeed = 40f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * horizontalSpeed;
        float vMovement = Input.GetAxis("Vertical") * verticalSpeed;
        rb.velocity = new Vector3(hMovement, 0, vMovement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();
        }
    }
}
