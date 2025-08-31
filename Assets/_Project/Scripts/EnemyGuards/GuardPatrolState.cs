using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GuardPatrolState : MonoBehaviour
{
    [SerializeField] float waitTime;
    private int currentPatrolIndex;

    public Coroutine Patrol;

    public List<Transform> patrolPoints;
    private NavMeshAgent agent;


    bool waiting = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void UpdatePatrolState()
    {
        if (!waiting && agent.remainingDistance <= agent.stoppingDistance)
        {
            Patrol = StartCoroutine(WaitAndGoToNextPoint());
        }
    }

    IEnumerator WaitAndGoToNextPoint()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);

        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Count)
        {
            currentPatrolIndex = 0; // loop
        }

        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        waiting = false;
    }

    public void GoToClosestPatrolPoint()
    {
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
    
}
