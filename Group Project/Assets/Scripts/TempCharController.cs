using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCharController : MonoBehaviour
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed / 2f;
        float vMovement = Input.GetAxis("Vertical") * movementSpeed;
        transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnTriggerEntered();
        }
    }
}
