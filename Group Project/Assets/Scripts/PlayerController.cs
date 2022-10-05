﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform feet;

    private CharacterController controller;

    private const float rotationSpeed = 140f;
    [SerializeField]private float movementSpeed = 50f;

    // For detecting whether the player can jump
    private const float rayDistance = 0.5f;

    private const float jumpForce = 0.07f;
    private const float gravity = 0.2f;
    private float velocityY = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Rotate player based on keyboard input first
        transform.Rotate(Vector3.up, rotation * Time.deltaTime);

        // Find the floor to know whether the player can jump
        RaycastHit hit;
        if (Physics.Raycast(feet.position + Vector3.up * rayDistance, Vector3.down, out hit, rayDistance * 2f))
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocityY = jumpForce;
            }
        }
        else
        {
            // Manually simulate gravity since CharacterController doesn't support it
            velocityY -= gravity * Time.deltaTime;
        }

        Vector3 velocity = transform.forward * movementSpeed * Time.deltaTime;
        controller.Move(velocity + Vector3.up * velocityY);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            RoadSpawner.instance.TriggerEntered();
        }
        else if (other.CompareTag("DeathTrigger"))
        {
            ScoreManager.instance.Die();
        }
    }

    public void AddSpeed(float amount)
    {
        movementSpeed += amount;
    }
    
    public float GetSpeed()
    {
        return movementSpeed;
    }
}
