using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SundaePickup : MonoBehaviour
{
    private const float rotationSpeed = 100f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float time = 3.0f;
    [SerializeField] private GameObject particle;

    private void Start()
    {
        // Random initial powerup rotation
        transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }

    private void Update()
    {
        // Make powerup spin for fun
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.SpeedPowerUp(speedMultiplier, time);
            ScoreManager.instance.PowerUpPopup("SILLY SUNDAE!");
            Instantiate(particle);
            Destroy(gameObject);
        }
    }
}
