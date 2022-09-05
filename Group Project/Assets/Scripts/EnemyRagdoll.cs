using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRagdoll : MonoBehaviour
{
    private Animator characterAnimator;
    [SerializeField] private Rigidbody characterRB;
    [SerializeField] private float upForce = 1000;
    [SerializeField] private float forwardForce = 500;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float leapRange = 7;

    public Transform player;

    public Transform parent;

    bool usedLeap = false;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Vector3 rayDirection = player.position - transform.position;

        if (Physics.Raycast(transform.position, rayDirection, out hit) && player != null )
        {
            if (hit.transform == player)
            {

                if (Vector3.Distance(transform.position, player.position) <= leapRange && !usedLeap )
                {
                    transform.LookAt(player.transform);
                    Leap();
                }

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

        //apply force
      
        characterRB.AddForce(gameObject.transform.up * upForce, ForceMode.Impulse);
        characterRB.AddForce(gameObject.transform.forward * forwardForce, ForceMode.Impulse);

        //disable animator
        DisableAnimation();

    }

    private void Run()
    {
        //characterRB.velocity = transform.forward * moveSpeed;
        parent.transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        //characterRB.MovePosition(transform.position + (transform.forward * moveSpeed * Time.deltaTime));
    }

    private void DisableAnimation()
    {
        characterAnimator.enabled = false;
    }


}
