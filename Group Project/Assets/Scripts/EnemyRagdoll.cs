using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    private Animator characterAnimator;
    private Transform player;

    [SerializeField] private Rigidbody characterRB;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float upForce = 100000f;
    [SerializeField] private float forwardForce = 5000f;


    private bool usedLeap = false;

    void Start()
    {
        characterAnimator = GetComponentsInChildren<Animator>()[0];
        player = GameObject.FindWithTag("Player").transform;
        characterRB = transform.Find("Kaleb").GetComponent<Rigidbody>();
        characterTransform = transform.Find("Kaleb").transform;
    }

    void Update()
    {
       
       if (Vector3.Distance(transform.position , player.position) < 40.0f && !usedLeap)
       {
            Debug.Log("leap plz");
                //transform.LookAt(player.transform);
            Leap();
       }else  if (!usedLeap)
       {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z -0.791f);
            characterTransform.position = transform.position;
       }
           
        
    }

    private void Leap()
    {

        usedLeap = true;
        //disable animator
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<NavAgentScript>().enabled = false;
        characterAnimator.enabled = false;
      
       


        // Apply force
        characterRB.AddForce(characterTransform.up * upForce, ForceMode.Impulse);
        characterRB.AddForce(characterTransform.forward * forwardForce, ForceMode.Impulse);



       
    }

 
}
