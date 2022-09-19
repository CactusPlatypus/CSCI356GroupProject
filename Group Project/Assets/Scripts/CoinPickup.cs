using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private CoinText coinLabel;
    private PlayerController player;
    private const float rotationSpeed = 100f;

    private void Start()
    {
        coinLabel = GameObject.FindWithTag("CoinText").GetComponent<CoinText>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        // Randomize initial coin rotation
        transform.Rotate(Vector3.forward, Random.Range(0f, 360f));
    }

    private void Update()
    {
        // Make coin spin for fun
        transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinLabel.add(1);
            player.addSpeed(1);
            Destroy(gameObject);
        }
    }
}
