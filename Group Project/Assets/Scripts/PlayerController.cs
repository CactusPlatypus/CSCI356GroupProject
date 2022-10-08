using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform feet;

    private CharacterController controller;

    private const float rotationSpeed = 140f;

    private const float maxSpeed = 200f;
    
    private float touchMoveAmount = 75f;
    private float touchControl = 0f;
    private float movementSpeed = 50f;
    private float speedMultiplier = 1f;

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
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            if(touch.position.x > 500){
                touchControl = touchMoveAmount;  // Turn Right
            }
            else if(touch.position.x < 500){
                touchControl = -touchMoveAmount; // Turn Left
            }
        } 
        else{
            touchControl = 0; // Go straight
        }

        float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) + touchControl; // Touch control is 0 when played on a PC

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

        // Speed up as game continues
        // note(hallam): use deltaTime or high FPS players speed up faster
        AddSpeed(Time.deltaTime * 0.4f);

        Vector3 velocity = transform.forward * movementSpeed * speedMultiplier * Time.deltaTime;
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
