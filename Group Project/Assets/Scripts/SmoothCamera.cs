using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;

    private Camera cam;
    private Vector3 offset = new Vector3(0f, 3f, 4f);
    private float positionSharpness = 5f;
    private float rotationSharpness = 5f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * positionSharpness);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * rotationSharpness);
        cam.fieldOfView = 60f + Vector3.Distance(transform.position, targetPosition) * 2f;
    }
}