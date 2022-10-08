using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Transform feet;

    private CharacterController controller;

    private const float rotationSpeed = 140f;
    private float touchControl = 0f;

    private float movementSpeed = 50f;
    private float speedMultiplier = 1f;
    private const float maxSpeed = 200f;

    // For detecting whether the player can jump
    private const float rayDistance = 0.5f;

    private bool grounded = false;
    private float timeSinceGrounded = 0f;

    private const float jumpForce = 40f;
    private const float gravity = 120f;
    private float velocityY = 0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                // Turn left or right scaled by rotationSpeed
                touchControl = touch.position.x > Screen.width * 0.5f ? 1f : -1f;
            }
        }
        else
        {
            touchControl = 0f; // Go straight
        }

        // Touch control is 0 when played on a PC
        float rotation = (Input.GetAxis("Horizontal") + touchControl) * rotationSpeed;

        // Rotate player based on keyboard input first
        transform.Rotate(Vector3.up, rotation * Time.deltaTime);

        if (grounded)
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

        // Speed up as game continues
        // note(hallam): use deltaTime or high FPS players speed up faster
        AddSpeed(0.4f * Time.deltaTime);

        Vector3 velocity = transform.forward * movementSpeed * speedMultiplier + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Find the floor to know whether the player can jump
        grounded = Physics.Raycast(feet.position + Vector3.up * rayDistance, Vector3.down, rayDistance * 2f);

        // Check if player is off the road for more than a second
        if (Physics.Raycast(feet.position + Vector3.up * rayDistance, Vector3.down))
        {
            timeSinceGrounded = 0f;
        }
        else
        {
            timeSinceGrounded += Time.deltaTime;
            // Kill player after 1 second off road
            if (timeSinceGrounded > 1f)
            {
                ScoreManager.instance.Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            RoadSpawner.instance.TriggerEntered();
        }
    }

    public void AddSpeed(float amount)
    {
        movementSpeed = Math.Min(movementSpeed + amount, maxSpeed);
    }
    
    public float GetSpeed()
    {
        return movementSpeed;
    }

    public void SetSpeedMultiplier(float multiplier, float time)
    {
        speedMultiplier = multiplier;
        Invoke("ResetSpeedMultiplier", time);
    }

    private void ResetSpeedMultiplier()
    {
        speedMultiplier = 1f;
    }
}
