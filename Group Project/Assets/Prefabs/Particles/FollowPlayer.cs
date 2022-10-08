using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    void Start()
    {
        transform.parent = GameObject.FindWithTag("Player").transform;
        transform.localPosition = Vector3.zero;
    }
}
