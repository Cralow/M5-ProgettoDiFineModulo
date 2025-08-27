using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterDetection : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    [Range(0f, 360f)] public float angle = 90f;  
    public float distance = 10f;                 

    public GameObject playerRef;

    public LayerMask targetMask;      
    public LayerMask obstructionMask; 

    public bool canseePlayer;

    private const float delay = 0.2f;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player")?.gameObject;
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
        }
    }

}