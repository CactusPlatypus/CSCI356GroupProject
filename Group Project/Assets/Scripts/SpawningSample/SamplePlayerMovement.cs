using UnityEngine;
using System.Collections;

public class SamplePlayerMovement : MonoBehaviour
{

    public float playerSpeed;
    public float sprintSpeed = 4f;
    public float walkSpeed = 2f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 5f;
    private bool isMoving = false;
    private bool isSprinting = false;
    private float yRot;

    private float maxSpeed = 30f;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {

        playerSpeed = walkSpeed;
        rigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);

        isMoving = false;

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f || Input.GetAxisRaw("Horizontal") != 0)
        {
            //transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * playerSpeed);
            if(rigidBody.velocity.magnitude < maxSpeed)
            {
                rigidBody.velocity += transform.right * Input.GetAxisRaw("Horizontal") * playerSpeed;
            }
            isMoving = true;
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f || Input.GetAxisRaw("Vertical") != 0)
        {
            //transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * playerSpeed);
            if (rigidBody.velocity.magnitude < maxSpeed)
            {
                rigidBody.velocity += transform.forward * Input.GetAxisRaw("Vertical") * playerSpeed;
            }
            isMoving = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.velocity += transform.up * jumpHeight;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            playerSpeed = walkSpeed;
            isSprinting = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
