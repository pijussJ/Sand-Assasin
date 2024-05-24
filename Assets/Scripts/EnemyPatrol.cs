using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    public float minDistanceToTarget;
    public List<Transform> targets = new List<Transform>();

    int currentIndex = 0;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        NextDestination();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, agent.destination) < minDistanceToTarget)
        {
            NextDestination();
            print("yippe");
        }
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
