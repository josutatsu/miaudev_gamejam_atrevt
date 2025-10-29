using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observar : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        // Busca al objeto con el tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    void Update()
    {
        // Si se encontró al jugador, el objeto lo observa
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f); // velocidad ajustable
        }
    }
}
