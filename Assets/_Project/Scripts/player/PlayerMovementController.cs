using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    //public float manualSpeed = 5f;
    public string groundTag = "Ground";

    [SerializeField] public float moveCheck = 0.05f;

    private NavMeshAgent agent;
    private Vector3 movementInput;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public float CurrentSpeed
    {
        get
        {
            Vector3 vel = new Vector3(agent.velocity.x, 0f, agent.velocity.z);
            return vel.magnitude;
        }
    }
    public bool isMoving => CurrentSpeed > moveCheck;


    void Update()
    {
        HandleMouseClick();
        HandleWASDInput();

        if (movementInput.magnitude > 0.1f)
        {
            agent.ResetPath();

            Vector3 move = movementInput * agent.speed * Time.deltaTime;
            agent.Move(move);
        }
    }

    void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(groundTag))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    void HandleWASDInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movementInput = new Vector3(h, 0, v).normalized;
    }
}