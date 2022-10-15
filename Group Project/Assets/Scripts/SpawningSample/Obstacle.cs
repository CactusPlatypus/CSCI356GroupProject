using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddCoins(-1);

            Destroy(gameObject);
        }
    }
}
