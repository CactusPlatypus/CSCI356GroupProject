using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoin : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        GameObject.FindWithTag("CoinText").GetComponent<CoinText>().add(1);
        //GameObject.FindWithTag("Player").GetComponent<CharacterController>().horizontalSpeed += 1;
        //GameObject.FindWithTag("Player").GetComponent<CharacterController>().verticalSpeed += 1;
        Destroy(gameObject);
    }
}
