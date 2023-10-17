using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Patrol : MonoBehaviour
{
    public List<Transform> patrolPoints;

    [SerializeField]
    private int indexOfNextPatrolPoint = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    private bool patrolling;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        // Initially set patrolling to true
        patrolling = true;

        GotoFirstPoint();  
    }

    private void OnEnable()
    {
        // Register to be notified of the OnTogglePatrol event and when
        // this event occurs call the TogglePatrolling function
        GameManager.OnTogglePatrol += TogglePatrolling;
    }

    private void OnDisable()
    {
        // Derigister (unsubscribe) from the event
        GameManager.OnTogglePatrol -= TogglePatrolling;
    }

    void GotoFirstPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(patrolPoints[0].position);
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Count == 0)
            return;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        indexOfNextPatrolPoint = (indexOfNextPatrolPoint + 1) % patrolPoints.Count;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(patrolPoints[indexOfNextPatrolPoint].position);
    }


    void Update()
    {
        if (patrolling)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            // agent.pathPending will be true if the agent is currently
            // calculating a path.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
    }

    public void StopPatrolling()
    {
        patrolling = false;
        agent.ResetPath();
    }

    public void StartPatrolling()
    {
        agent.SetDestination(patrolPoints[indexOfNextPatrolPoint].position);
        patrolling = true;
    }

    private void TogglePatrolling()
    {
       if (patrolling)
        {
            StopPatrolling();
        }
        else
        {
            StartPatrolling();
        }
    }
}
