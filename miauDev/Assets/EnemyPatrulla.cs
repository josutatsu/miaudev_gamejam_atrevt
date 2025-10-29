using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyPatrulla : MonoBehaviour
{
    public Transform[] patrolPoints; // Puntos de patrulla
    public float detectionRange = 10f; // Distancia a la que detecta al jugador
    public float chaseRange = 15f;     // Distancia máxima antes de volver a patrullar
    public float waitTime = 2f;        // Tiempo de espera entre puntos

    private int currentPoint = 0;
    private NavMeshAgent agent;
    private Transform player;
    private float waitCounter;
    private bool waiting;
    private bool chasing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            chasing = true;
        }
        // Si el jugador se aleja más del rango de persecución
        else if (distanceToPlayer >= chaseRange)
        {
            chasing = false;
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (waiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0)
            {
                waiting = false;
                GoToNextPoint();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waiting = true;
            waitCounter = waitTime;
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPoint = (currentPoint + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPoint].position);
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dibuja los rangos de detección en el editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
