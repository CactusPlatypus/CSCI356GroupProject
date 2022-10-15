using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeap : MonoBehaviour
{
    [SerializeField] private Rigidbody headRB;
    [SerializeField] private float upForce = 100f;
    [SerializeField] private float forwardForce = 120f;
    void Start()
    {
        headRB.AddForce(transform.up * upForce, ForceMode.Impulse);
        headRB.AddForce(transform.forward * forwardForce, ForceMode.Impulse);
    }

  
}
