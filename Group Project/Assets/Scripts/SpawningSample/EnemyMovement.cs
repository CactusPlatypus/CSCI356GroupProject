using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
	Waiting,
	Pursuing,
	Leaping,
}
public class EnemyMovement : MonoBehaviour
{
	public float distanceToChase = 10f;
	public EnemyState currentState = EnemyState.Waiting;
	private NavMeshAgent agent;
	private Transform target;

	private void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		target = GameObject.FindWithTag("Player").transform;
	}

	private void FixedUpdate()
	{

		if (currentState == EnemyState.Leaping)
        {
			return;
        }

		if (Vector3.Distance(gameObject.transform.position, target.position) <= distanceToChase)
		{
			currentState = EnemyState.Pursuing;
        }
        else
        {
			currentState = EnemyState.Waiting;
		}

		if (currentState == EnemyState.Pursuing)
        {
			if (agent.isOnNavMesh || agent.isOnOffMeshLink)
			{
				agent.SetDestination(target.position);
			}
		}

	}
}
