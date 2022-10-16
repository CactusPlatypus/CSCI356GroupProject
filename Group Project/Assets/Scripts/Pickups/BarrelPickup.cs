using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPickup : MonoBehaviour
{
    private const float spinSpeed = 100f;
    [SerializeField] private float lifetime = 9f;
    [SerializeField] private GameObject spinningBarrels;

    private void Start()
    {
        // Random initial powerup rotation
        transform.Rotate(Vector3.up, Random.Range(0f, 360f));
    }

    private void Update()
    {
        // Make powerup spin for fun
        transform.Rotate(Vector3.up, Time.deltaTime * spinSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.PowerUpPopup("BONKERS BARRELS!");
            ScoreManager.instance.InvinciblePowerUp(lifetime);
            Transform player = GameObject.FindWithTag("Player").transform;

            GameObject barrels = Instantiate(spinningBarrels, player);
            barrels.transform.localPosition = Vector3.zero;


            // Destroy barrels after lifetime expires
            Destroy(barrels, lifetime);

            Destroy(gameObject);
        }
    }
}
