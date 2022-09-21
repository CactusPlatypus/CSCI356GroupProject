using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float time = 2.0f;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
