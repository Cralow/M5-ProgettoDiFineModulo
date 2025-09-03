using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GuardRotateState : MonoBehaviour
{
    [SerializeField] private Quaternion endRotation;
    [SerializeField] private Quaternion startRotation;
    [SerializeField] private float duration;
    float timer = 0f;
    float rate;

    [SerializeField] private float rotationDelay;
    [SerializeField] private Vector3 checkPointPos;
    [SerializeField] private Quaternion checkPointRot;
    public Coroutine Rotation;

    NavMeshAgent agent;
    public bool waiting = false;
    [SerializeField] private bool isReturningToCheckPoint;
    bool turn = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        checkPointPos = transform.position;
        checkPointRot = transform.rotation;
    }

    public void UpdateGuardState()
    {
        if (!waiting && !isReturningToCheckPoint)
        {
            Rotation = StartCoroutine(GuardRotationPatrol());
        }
        else if (isReturningToCheckPoint)
        {
            StopCoroutine(Rotation);
        }
    }

    IEnumerator GuardRotationPatrol()
    {
        waiting = true;
        yield return new WaitForSeconds(rotationDelay);

        float angle = turn ? 180f : -0;
        Quaternion rotation = Quaternion.Euler(0,angle, 0);

        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            rate = timer / duration; // 0 all'inizio -> 1 alla fine
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rate);
        }
        timer = 0;

        waiting = false;
        turn = !turn;
    }
    public void GoToCheckPoint()
    {
        agent.SetDestination(checkPointPos);
        if (agent.hasPath)
        {
            isReturningToCheckPoint = true;
        }
        else
        {
            transform.rotation = isReturningToCheckPoint ? checkPointRot:transform.rotation;
            isReturningToCheckPoint = false;

        }
        
    }
}
