using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinePickup : MonoBehaviour
{
    private const float spinSpeed = 100f;
    [SerializeField] private GameObject wineWarp;

    private void Start()
    {
        // Random initial pickup rotation
        transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }

    private void Update()
    {
        // Make pickup spin for fun
        transform.Rotate(Vector3.up, Time.deltaTime * spinSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.PowerUpPopup("Wine Warp!");
            Instantiate(wineWarp);
            Destroy(gameObject);
        }
    }
}
