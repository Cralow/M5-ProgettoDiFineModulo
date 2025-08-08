using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private PlayerMovementController playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovementController>();
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    // qui controlli da playermove moveChek e basta che lo assegni a setfloat anim 
}