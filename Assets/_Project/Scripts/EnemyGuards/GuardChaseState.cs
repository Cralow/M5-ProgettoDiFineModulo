using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static PatrolController;

public class GuardChaseState : MonoBehaviour
{
    private GuardController controller;
    private NavMeshAgent agent;
    private GameObject player;

    [SerializeField] private float timeSinceLastSeen;
    [SerializeField] private float loseTargetTime;
    public bool canSeePlayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
    }
    public void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);

        if (canSeePlayer)
        {
            timeSinceLastSeen = 0f;
        }
        else
        {
            timeSinceLastSeen += Time.deltaTime;
            if (timeSinceLastSeen >= loseTargetTime)
            {
                canSeePlayer = false;
                controller.currentState = EnemyState.Patrol;

            }
        }

    }
}
