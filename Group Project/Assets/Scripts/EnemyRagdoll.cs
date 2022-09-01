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

    public Transform parent;

    float runTime = 3f;
    bool usedLeap = false;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        runTime -= Time.deltaTime;

        if (runTime <= 0 && !usedLeap)
        {
            Leap();
        }
        else if (runTime > 0)
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
