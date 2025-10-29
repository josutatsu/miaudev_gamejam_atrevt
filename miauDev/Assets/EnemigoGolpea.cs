using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGolpea : MonoBehaviour
{
    [Header("Da�o al jugador")]
    public float damagePerSecond = 10f; // da�o continuo
    public float hitInterval = 1f;      // cada cu�ntos segundos da�a

    private bool playerInRange = false;
    private float nextDamageTime = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        if (playerInRange && Time.time >= nextDamageTime)
        {
            nextDamageTime = Time.time + hitInterval;

            // Buscar el script del jugador y hacerle da�o
            inputSystemP player = FindObjectOfType<inputSystemP>();
            if (player != null)
                player.RecibirDa�o(damagePerSecond);
        }
    }
}
