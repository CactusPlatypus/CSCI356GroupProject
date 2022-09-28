using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotdogPickup : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreController;
    private const float rotationSpeed = 100f;
    [SerializeField] private float scoreMultiplier = 2.0f;
    [SerializeField] private float time = 10.0f;

    private void Start()
    {
        // Random initial coin rotation
        transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        scoreController = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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
            scoreController.setScoreMultiplier(2.0f, time);
            scoreController.powerUpPopUp("DOUBLE DAWGS!");

            Destroy(gameObject);
        }
    }
}
