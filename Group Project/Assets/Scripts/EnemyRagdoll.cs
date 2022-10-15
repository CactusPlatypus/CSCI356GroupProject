using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    private Transform player;

    [SerializeField] private Rigidbody characterRB;
    [SerializeField] private float upForce = 1000f;
    [SerializeField] private float forwardForce = 500f;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float leapRange = 7f;

    private bool usedLeap = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= leapRange && !usedLeap)
        {
            RaycastHit hit;
            Vector3 rayDirection = player.position - transform.position;
            if (Physics.Raycast(transform.position, rayDirection, out hit) && hit.transform == player)
            {
                transform.LookAt(player.transform);
                Leap();
            }
        }

        if (!usedLeap)
        {
            Run();
        }
    }

    private void Leap()
    {
        usedLeap = true;

        //change animation to jump
        //characterAnimator.SetTrigger("Jump");

        // Apply force
        characterRB.AddForce(transform.up * upForce, ForceMode.Impulse);
        characterRB.AddForce(transform.forward * forwardForce, ForceMode.Impulse);

        //disable animator
        characterAnimator.enabled = false;
    }

    private void Run()
    {
        //characterRB.velocity = transform.forward * moveSpeed;
        transform.parent.Translate(transform.forward * moveSpeed * Time.deltaTime);
        //characterRB.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
    }
}
