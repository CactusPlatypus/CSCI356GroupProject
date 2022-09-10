using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharController : MonoBehaviour
{
    public RoadSpawner spawnManager;
    public Transform feet;

    private const float horizontalSpeed = 100f;
    private const float verticalSpeed = 50f;

    private const float rayDistance = 0.5f;
    private const float rotationSharpness = 10f;

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * horizontalSpeed;
        //float vMovement = Input.GetAxis("Vertical") * verticalSpeed;

        // Rotate player based on keyboard input first
        transform.Rotate(Vector3.up, hMovement * Time.deltaTime);

        // Calculate the velocity to know the next expected position
        Vector3 velocity = transform.forward * verticalSpeed * Time.deltaTime;

        // Send a ray downwards above the next expected position to find the floor
        RaycastHit hit;
        if (Physics.Raycast(feet.position + velocity + transform.up * rayDistance, -transform.up, out hit, rayDistance * 2f))
        {
            // Rotate to sit on the floor, hopefully this doesn't break
            Vector3 newRight = Vector3.Cross(hit.normal, transform.forward);
            Vector3 newForward = Vector3.Cross(newRight, hit.normal);
            Quaternion newRotation = Quaternion.LookRotation(newForward, hit.normal);

            // Smooth rotation to avoid bumps due to janky models
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSharpness);

            // No idea if position should happen before or after rotation
            transform.position += hit.point - feet.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.TriggerEntered();
        }
    }
}
