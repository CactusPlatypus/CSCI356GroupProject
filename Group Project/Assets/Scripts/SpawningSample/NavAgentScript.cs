using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentScript : MonoBehaviour {
	public float distanceToChase = 10;
	public Transform target;
	private NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = PlayerObject.GetPlayerObject().transform;
		SetTarget();
	}
	

	void FixedUpdate () {
		SetTarget();
	}

	void SetTarget()
    {
		if(target == null)
        {
			target = PlayerObject.GetPlayerObject().transform;
		}

		if(target == null)
        {
			return;
        }

		if(Vector3.Distance(gameObject.transform.position, target.position) <= distanceToChase)
        {
			agent.SetDestination(target.position); // if the target is static
        }

	}
}
