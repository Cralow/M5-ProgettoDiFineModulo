using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    private GuardChaseState chaseState;
    private GuardPatrolState patrolState;
    public enum EnemyStates { Patrol, Chase }
    public EnemyStates currentState;


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
            case EnemyStates.Patrol:
                patrolState.UpdatePatrolState();
                break;
            case EnemyStates.Chase:
                chaseState.ChasePlayer();
                break;
        }
    }
    public void EnterState(EnemyStates enemyState)
    {
        currentState = enemyState;
    }

}
