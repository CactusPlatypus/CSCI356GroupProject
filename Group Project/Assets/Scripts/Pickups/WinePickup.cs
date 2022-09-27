using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinePickup : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreController;
    private const float rotationSpeed = 100f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float time = 9.0f;
    [SerializeField] private GameObject wineWarp;

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

            scoreController.powerUpPopUp("Wine Warp!");
            Instantiate(wineWarp);


            Destroy(gameObject);
        }
    }
}
