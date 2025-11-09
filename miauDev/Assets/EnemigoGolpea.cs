using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGolpea : MonoBehaviour
{
    [Header("Daño al jugador")]
    public float damagePerSecond = 10f; // daño continuo
    public float hitInterval = 1f;      // cada cuántos segundos daña

    [Header("Sonido de golpe")]
    public AudioClip golpeSonido;        // asignar en el inspector
    private AudioSource audioSource;     // componente de audio

    private bool playerInRange = false;
    private float nextDamageTime = 0f;

    void Start()
    {
        // Agrega un AudioSource al enemigo si no tiene uno
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

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

            // Buscar el script del jugador y hacerle daño
            inputSystemP player = FindObjectOfType<inputSystemP>();
            if (player != null)
                player.RecibirDaño(damagePerSecond);

            // Reproducir sonido de golpe
            if (golpeSonido != null)
                audioSource.PlayOneShot(golpeSonido);
        }
    }
}
