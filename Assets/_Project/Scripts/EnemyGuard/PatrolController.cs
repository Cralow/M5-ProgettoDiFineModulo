using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    public enum EnemyState { Patrol, Chase }
    private EnemyState currentState;

    [Header("Patrol Settings")]
    public List<Transform> patrolPoints;
    public float waitTime = 2f;
    public bool loop = true;

    [Header("Chase Settings")]
    public float detectionRadius = 10f;
    public float loseTargetTime = 3f;

    private NavMeshAgent agent;
    private Transform player;

    private int currentPatrolIndex = 0;
    private bool waiting = false;
    private float timeSinceLastSeen = Mathf.Infinity;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        EnterState(EnemyState.Patrol);
    }

    void Update()
    {
        if (player == null) return;

        UpdateState();
    }

    void EnterState(EnemyState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Patrol:
                GoToClosestPatrolPoint();
                break;
            case EnemyState.Chase:
                break;
        }
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                UpdatePatrolState();
                break;
            case EnemyState.Chase:
                UpdateChaseState();
                break;
        }
    }

    void UpdatePatrolState()
    {
        if (CanSeePlayer())
        {
            EnterState(EnemyState.Chase);
            return;
        }

        if (patrolPoints.Count == 0 || waiting || agent.pathPending) return;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            StartCoroutine(WaitAndGoToNextPoint());
        }
    }

    void UpdateChaseState()
    {
        if (player == null) return;

        agent.SetDestination(player.position);

        if (CanSeePlayer())
        {
            timeSinceLastSeen = 0f;
        }
        else
        {
            timeSinceLastSeen += Time.deltaTime;
            if (timeSinceLastSeen >= loseTargetTime)
            {
                EnterState(EnemyState.Patrol);
            }
        }
    }

    IEnumerator WaitAndGoToNextPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Count)
        {
            currentPatrolIndex = loop ? 0 : patrolPoints.Count - 1;
        }

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        waiting = false;
    }

    bool CanSeePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= detectionRadius;
    }

    public void GoToClosestPatrolPoint()
    {
        if (patrolPoints.Count == 0) return;

        float closestDistance = Mathf.Infinity;
        int closestIndex = 0;

        for (int i = 0; i < patrolPoints.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closestIndex = i;
            }
        }

        currentPatrolIndex = closestIndex;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
