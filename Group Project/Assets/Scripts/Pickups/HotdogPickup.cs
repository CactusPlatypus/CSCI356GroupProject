using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogPickup : MonoBehaviour
{
    private const float rotationSpeed = 100f;
    [SerializeField] private float time = 10f;

    private void Start()
    {
        // Random initial coin rotation
        transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }

    private void Update()
    {
        // Make coin spin for fun
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.SetScoreMultiplier(2, time);
            ScoreManager.instance.PowerUpPopup("DOUBLE DAWGS!");
            Destroy(gameObject);
        }
    }
}
