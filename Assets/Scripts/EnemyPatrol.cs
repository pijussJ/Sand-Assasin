using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float minDistanceToTarget;
    public float playerEscapeDelay;

    public List<Transform> targets = new List<Transform>();

    int currentIndex = 0;

    bool canSeePlayerCopy;
    float delayCopy;

    NavMeshAgent agent;

    EnemyFieldOfView fov;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        fov = GetComponent<EnemyFieldOfView>();

        NextDestination();

        delayCopy = playerEscapeDelay;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) <= minDistanceToTarget)
        {
            NextDestination();
        }

        if (fov.canSeePlayer)
        {
            agent.destination = fov.player.transform.position;
        }
        else if (canSeePlayerCopy)
        {
            delayCopy -= Time.deltaTime;

            if (delayCopy > 0)
                return;
            else
            {
                agent.destination = targets[currentIndex].position;
                delayCopy = playerEscapeDelay;
            }
        }

        canSeePlayerCopy = fov.canSeePlayer;
    }

    private void NextDestination()
    {
        agent.destination = targets[currentIndex].position;

        currentIndex++;

        if (currentIndex >= targets.Count)
        {
            currentIndex = 0;
        }
    }
}
