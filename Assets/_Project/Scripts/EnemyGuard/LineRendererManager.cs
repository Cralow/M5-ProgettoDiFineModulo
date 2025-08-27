using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererManager : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private CharaterDetection charaterDetection;
    [SerializeField] private Transform headTransform;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        charaterDetection = GetComponent<CharaterDetection>();
    }

    private void FixedUpdate()
    {
        EvaluteConeView(12);
    }

    public void EvaluteConeView(int subdivisions)
    {
        if (charaterDetection == null || lineRenderer == null) return;
        if (subdivisions < 1) subdivisions = 1;

        int points = subdivisions + 1;
        lineRenderer.positionCount = points + 1;

        Vector3 origin = headTransform.transform.position + new Vector3(0f, 0.05f, 0f);
        float viewAngle = charaterDetection.angle;
        float maxDist = charaterDetection.distance;
        float half = viewAngle * 0.5f;

        lineRenderer.SetPosition(0, origin);

        for (int i = 0; i <= subdivisions; i++)
        {
            float t = (float)i / subdivisions;
            float ang = -half + t * viewAngle;
            Vector3 dir = Quaternion.Euler(0f, ang, 0f) * transform.forward;

            Vector3 end = origin + dir * maxDist;

            if (Physics.Raycast(origin, dir, out RaycastHit hit, maxDist, charaterDetection.obstructionMask))
            {
                end = hit.point;
            }

            lineRenderer.SetPosition(i + 1, end);
        }
    }
}