using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform feet;

    private CharacterController controller;

    private const float rotationSpeed = 140f;
    private const float movementSpeed = 60f;

    private const float rayDistance = 0.5f;
    //private const float rotationSharpness = 10f;

    private const float gravity = 0.2f;
    private float velocityY = 0f;
    private float speedBoost = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make player move faster as they get coins
        float movement = movementSpeed + speedBoost;

        // Rotate player based on keyboard input first
        transform.Rotate(Vector3.up, rotation * Time.deltaTime);

        // Calculate the velocity to know the next expected position
        Vector3 velocity = transform.forward * movement * Time.deltaTime;

        // Send a ray downwards above the next expected position to find the floor
        RaycastHit hit;
        if (Physics.Raycast(feet.position + Vector3.up * rayDistance, Vector3.down, out hit, rayDistance * 2f))
        {
            // Rotate to sit on the floor, hopefully this doesn't break
            /*Vector3 newRight = Vector3.Cross(hit.normal, transform.forward);
            Vector3 newForward = Vector3.Cross(newRight, hit.normal);
            Quaternion newRotation = Quaternion.LookRotation(newForward, hit.normal);

            // Smooth rotation to avoid bumps due to janky models, note this screws up the raycast
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSharpness);

            // No idea if position should happen before or after rotation
            //transform.position += hit.point - feet.position;*/

            if (Input.GetButtonDown("Jump"))
            {
                velocityY = 0.05f;
            }
        }
        else
        {
            velocityY -= gravity * Time.deltaTime;
        }

        controller.Move(velocity + Vector3.up * velocityY);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            RoadSpawner.instance.TriggerEntered();
        }
    }

    public void addSpeed(float amount)
    {
        speedBoost += amount;
    }
}
