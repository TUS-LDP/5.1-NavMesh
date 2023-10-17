using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    public Transform player;
    public float stopRange;
    public float chaseRange;
    public float chaseSpeed;
    private bool chasing;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        // Rather than using the following ...
        //      player  = GameObject.FindGameObjectWithTag("Player").transform
        // ...which is a bit error prone as I may have spelt the "Player" tag incorrectly, I
        // am going to use the following which gets me the CharacterController object which
        // I know is attached to the playerCapsule (no spelling mistakes possible - if there
        // were any the code simply wouldn't compile.
        player = GameObject.FindObjectOfType<CharacterController>().transform;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            GameManager.instance.TogglePatrolling();
            StartChasing();
        }
        else if (distanceToPlayer < stopRange)
        {
            StopChasing();
        }
        else if (distanceToPlayer > chaseRange && chasing)
        {
            StopChasing();
            GameManager.instance.TogglePatrolling();
        }
    }

    public void StartChasing()
    {
        chasing = true;
        StartCoroutine(Chase());
    }

    public void StopChasing()
    {
        chasing = false;
    }

    public IEnumerator Chase()
    {
        WaitForSeconds waitTime = new WaitForSeconds(chaseSpeed);

        while (chasing)
        {
            agent.SetDestination(player.position);
            yield return waitTime;
        }

    }
}
