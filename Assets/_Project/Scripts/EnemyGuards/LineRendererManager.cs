using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform headTransform;
    private GuardController controller;
    private GuardChaseState chaseState;

    [SerializeField] private float radius = 10f;
    [SerializeField] private float distance = 8f;
    [Range(0f, 360f)] public float angle = 90f;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        controller = GetComponent<GuardController>();
        chaseState = GetComponent<GuardChaseState>();
    }

    private void FixedUpdate()
    {
        EvaluteConeView(12);
    }

    public void EvaluteConeView(int subdivisions)
    {
        if (subdivisions < 1) subdivisions = 1;

        int points = subdivisions + 1;
        lineRenderer.positionCount = points + 1;

        Vector3 origin = headTransform.transform.position + new Vector3(0f, 0.5f, 0f);
        float viewAngle = angle;
        float maxDist = distance;
        float half = viewAngle * 0.5f;

        lineRenderer.SetPosition(0, origin);

        chaseState.canSeePlayer = false;

        for (int i = 0; i <= subdivisions; i++)
        {
            float t = (float)i / subdivisions;
            float ang = -half + t * viewAngle;
            Vector3 dir = Quaternion.Euler(0f, ang, 0f) * transform.forward;

            Vector3 end = origin + dir * maxDist;

            if (Physics.Raycast(origin, dir, out RaycastHit hit, maxDist, obstructionMask))
            {
                end = hit.point;
            }
            else if (Physics.Raycast(origin, dir, out hit, maxDist, targetMask))
            {
                end = hit.point;
                controller.EnterState(GuardController.EnemyStates.Chase);
                chaseState.canSeePlayer = true;
            }


            lineRenderer.SetPosition(i + 1, end);
        }
    }
}