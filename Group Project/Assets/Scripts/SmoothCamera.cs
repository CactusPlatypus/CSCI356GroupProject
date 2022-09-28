using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Transform target;
    private Vector3 offset = new Vector3(0f, 4f, 3f);
    private float positionSharpness = 5f;
    private float rotationSharpness = 8f;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionSharpness);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSharpness);
    }
}