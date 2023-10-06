using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Patrol : MonoBehaviour
{
    public List<Transform> patrolPoints;
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

        patrolling = GameManager.instance.partollingEnabled;

        GotoNextPoint();

        // Register to be notified of the OnTogglePatrol event and when
        // this event occurs call the TogglePatrolling function;
        GameManager.instance.OnTogglePatrol += TogglePatrolling;
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Count == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(patrolPoints[indexOfNextPatrolPoint].position);

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        indexOfNextPatrolPoint = (indexOfNextPatrolPoint + 1) % patrolPoints.Count;
    }


    void Update()
    {
        if (GameManager.instance.partollingEnabled && patrolling)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            // agent.pathPending will be true if the agent is currently
            // calculating a path.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }
        else if (GameManager.instance.partollingEnabled && !patrolling)
        {
            StartPatrolling();
        }
        else if (!GameManager.instance.partollingEnabled && patrolling)
        {
            StopPatrolling();
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
        Debug.Log("Patrolling has been toggled");
    }
}
