using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentScript : MonoBehaviour
{
	public float distanceToChase = 10f;
	
	private NavMeshAgent agent;
	private Transform target;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		target = GameObject.FindWithTag("Player").transform;
	}

	private void FixedUpdate()
	{
		if (Vector3.Distance(gameObject.transform.position, target.position) <= distanceToChase)
		{
			agent.SetDestination(target.position); // If the target is static
		}
	}
}
