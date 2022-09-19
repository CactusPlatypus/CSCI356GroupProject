using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 offset = new Vector3(0f, 6f, 3f);
    private float positionSharpness = 5f;
    private float rotationSharpness = 8f;

    void LateUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionSharpness);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSharpness);
    }
}