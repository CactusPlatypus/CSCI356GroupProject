using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerController : MonoBehaviour
{
    public PathCreator pathCreator;
    public Transform bottom;

    private const float forwardMoveSpeed = 100f;
    private const float sideMoveSpeed = 50f;

    private const float roadWidth = 4.5f;
    private const float edgeGap = 0.5f;

    private float sideOffset = 0f;
    private float forwardOffset = edgeGap;

    private float verticalVelocity = 0f;
    private float verticalOffset = 0f;

    // Update is called once per frame
    void Update()
    {
        forwardOffset += Input.GetAxis("Vertical") * Time.deltaTime * forwardMoveSpeed;
        //forwardOffset = Mathf.Clamp(forwardOffset, edgeGap, pathCreator.path.length - edgeGap);

        sideOffset += Input.GetAxis("Horizontal") * Time.deltaTime * sideMoveSpeed;
        sideOffset = Mathf.Clamp(sideOffset, -roadWidth, roadWidth);

        Vector3 forwardPosition = pathCreator.path.GetPointAtDistance(forwardOffset, EndOfPathInstruction.Loop);
        Quaternion rotation = pathCreator.path.GetRotationAtDistance(forwardOffset, EndOfPathInstruction.Loop) * Quaternion.Euler(0, 0, 90);
        Vector3 sidePosition = sideOffset * (rotation * Vector3.right);
        Vector3 topPosition = verticalOffset * (rotation * Vector3.up);

        if (Input.GetButtonDown("Jump"))
        {
            verticalVelocity += 0.1f;
        }

        if (Mathf.Abs(verticalVelocity) > 0f)
        {
            verticalVelocity -= Time.deltaTime * 0.3f;
            verticalOffset += verticalVelocity;
            if (verticalOffset <= 0f)
            {
                verticalVelocity = 0f;
                verticalOffset = 0f;
            }
        }
        
        transform.position = forwardPosition + sidePosition - bottom.localPosition + topPosition;
        transform.rotation = rotation;
    }
}
