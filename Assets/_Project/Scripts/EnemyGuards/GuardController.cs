using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    private GuardChaseState chaseState;
    private GuardPatrolState patrolState;
    private GuardRotateState rotateState;
    public enum EnemyStates { Patrol, Chase }
    public EnemyStates currentState;


    private void Awake()
    {
        patrolState = GetComponent<GuardPatrolState>();
        rotateState = GetComponent<GuardRotateState>();
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
                if (patrolState != null)
                {
                  patrolState.UpdatePatrolState();
                }
                else
                {
                    rotateState.UpdateGuardState();
                }
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
