using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelSpin : MonoBehaviour
{
    private const float spinSpeed = 300f;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * spinSpeed);
    }
}
