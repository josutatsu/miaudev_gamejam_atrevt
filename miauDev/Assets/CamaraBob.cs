using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraBob : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public float bobSpeed = 2.0f;      // Velocidad del movimiento
    public float bobAmount = 0.05f;    // Intensidad del movimiento vertical
    public bool lateralMovement = true; // Activa/desactiva movimiento lateral

    private Vector3 startPos;
    private float timer = 0f;

    void Start()
    {
        // Guardamos la posición inicial de la cámara
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Movimiento tipo DOOM, constante y suave
        timer += Time.deltaTime * bobSpeed;

        float newY = startPos.y + Mathf.Sin(timer) * bobAmount;
        float newX = startPos.x;

        if (lateralMovement)
        {
            // Movimiento lateral opcional
            newX = startPos.x + Mathf.Cos(timer * 0.5f) * (bobAmount * 0.5f);
        }

        transform.localPosition = new Vector3(newX, newY, startPos.z);
    }
}
