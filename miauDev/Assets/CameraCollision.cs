using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player; // El objeto que la cámara sigue
    public float distance = 3f; // Distancia normal de la cámara al jugador
    public float smoothSpeed = 10f; // Qué tan suave se ajusta la cámara
    public LayerMask collisionMask; // Capas con las que la cámara debe chocar (paredes, objetos, etc.)

    private Vector3 cameraDirection;
    private Vector3 desiredPosition;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning(" No se asignó el jugador en CameraCollision.");
        }

        cameraDirection = transform.localPosition.normalized;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Dirección deseada (desde el jugador hacia atrás)
        cameraDirection = (transform.position - player.position).normalized;

        // Posición ideal de la cámara (distancia normal)
        desiredPosition = player.position - cameraDirection * distance;

        // Revisar colisión entre jugador y cámara
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredPosition, out hit, collisionMask))
        {
            // Si hay un obstáculo, coloca la cámara justo antes del impacto
            float adjustedDistance = Vector3.Distance(player.position, hit.point) - 0.1f;
            Vector3 newPosition = player.position - cameraDirection * adjustedDistance;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Si no hay obstáculos, mantén la distancia normal
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }

        // Siempre mirar hacia el jugador
        transform.LookAt(player);
    }
}
