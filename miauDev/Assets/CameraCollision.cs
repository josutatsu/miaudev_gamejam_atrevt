using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player; // El objeto que la c�mara sigue
    public float distance = 3f; // Distancia normal de la c�mara al jugador
    public float smoothSpeed = 10f; // Qu� tan suave se ajusta la c�mara
    public LayerMask collisionMask; // Capas con las que la c�mara debe chocar (paredes, objetos, etc.)

    private Vector3 cameraDirection;
    private Vector3 desiredPosition;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning(" No se asign� el jugador en CameraCollision.");
        }

        cameraDirection = transform.localPosition.normalized;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Direcci�n deseada (desde el jugador hacia atr�s)
        cameraDirection = (transform.position - player.position).normalized;

        // Posici�n ideal de la c�mara (distancia normal)
        desiredPosition = player.position - cameraDirection * distance;

        // Revisar colisi�n entre jugador y c�mara
        RaycastHit hit;
        if (Physics.Linecast(player.position, desiredPosition, out hit, collisionMask))
        {
            // Si hay un obst�culo, coloca la c�mara justo antes del impacto
            float adjustedDistance = Vector3.Distance(player.position, hit.point) - 0.1f;
            Vector3 newPosition = player.position - cameraDirection * adjustedDistance;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // Si no hay obst�culos, mant�n la distancia normal
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);
        }

        // Siempre mirar hacia el jugador
        transform.LookAt(player);
    }
}
