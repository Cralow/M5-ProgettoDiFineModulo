using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentieryGuard : MonoBehaviour
{
    [SerializeField] private float rotationAngle = 45f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float waitTime = 0.5f;
    [SerializeField] private float snap = 0.1f; 

    private void Start()
    {
        StartCoroutine(RotationLoop());
    }

    private IEnumerator RuotaVerso(Quaternion target)
    {
        while (Quaternion.Angle(transform.rotation, target) > snap)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                target,
                rotationSpeed * Time.deltaTime
            );
            yield return null;
        }
        transform.rotation = target;
    }

    private IEnumerator RotationLoop()
    {
        while (true)
        {
            Quaternion baseRot = transform.rotation;

            Quaternion rotationDx = baseRot * Quaternion.Euler(0f, rotationAngle, 0f);
            Quaternion rotationSx = baseRot * Quaternion.Euler(0f, -rotationAngle, 0f);

            yield return RuotaVerso(rotationDx);
            yield return new WaitForSeconds(waitTime);

            yield return RuotaVerso(rotationSx);
            yield return new WaitForSeconds(waitTime);
        }
    }
}