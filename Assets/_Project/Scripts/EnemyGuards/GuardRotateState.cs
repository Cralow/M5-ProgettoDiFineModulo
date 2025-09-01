using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuardRotateState : MonoBehaviour
{
    [SerializeField] private float delay;

    public Coroutine Rotation;
    public bool waiting = false;
    bool turn = false;

    public void UpdateGuardState()
    {
        if (!waiting)
        {
            Rotation = StartCoroutine(GuardRotationPatrol());
        }
    }

    IEnumerator GuardRotationPatrol()
    {
        waiting = true;
        yield return new WaitForSeconds(delay);

        float angle = turn ? 180f : -180f;
        

        waiting = false;
    }
}
