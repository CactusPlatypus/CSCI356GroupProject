using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRagdoll : MonoBehaviour
{
    private Animator characterAnimator;
    private Transform player;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float upForce = 100000f;
    [SerializeField] private float forwardForce = 120f;
    [SerializeField] private GameObject ragdoll;


    private bool usedLeap = false;

    void Start()
    {
        characterAnimator = GetComponentsInChildren<Animator>()[0];
        player = GameObject.FindWithTag("Player").transform;
        characterTransform = transform.Find("Kaleb").transform;
    }

    void Update()
    {
       
       if (Vector3.Distance(transform.position , player.position) < 25.0f && !usedLeap)
       {
            Debug.Log("leap plz");
                //transform.LookAt(player.transform);
            Leap();
       }else  if (!usedLeap)
       {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z -0.791f);
            characterTransform.position = transform.position;
       }
           
        
    }

    private void Leap()
    {

        usedLeap = true;
        //disable animator
  
        GameObject spawnedRagdoll = Instantiate(ragdoll);
        spawnedRagdoll.transform.position = transform.position;
        spawnedRagdoll.transform.LookAt(player);
        // Apply force
        spawnedRagdoll.GetComponent<Rigidbody>().AddForce(characterTransform.up * upForce, ForceMode.Impulse);
        spawnedRagdoll.GetComponent<Rigidbody>().AddForce(characterTransform.forward * forwardForce, ForceMode.Impulse);
        Destroy(gameObject);
    


        



       
    }

   
}
