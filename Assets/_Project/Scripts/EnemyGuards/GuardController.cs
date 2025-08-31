using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PatrolController;

public class GuardController : MonoBehaviour
{
    private GuardChaseState chaseState;
    private GuardPatrolState patrolState;
    public EnemyState currentState;


    private void Awake()
    {
        patrolState = GetComponent<GuardPatrolState>();
        chaseState = GetComponent<GuardChaseState>();
    }
    private void Update()
    {
        UpdateState();
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                patrolState.UpdatePatrolState();
                break;
            case EnemyState.Chase:
                patrolState.StopCoroutine(patrolState.Patrol);
                chaseState.ChasePlayer();
                break;
        }
    }


}
