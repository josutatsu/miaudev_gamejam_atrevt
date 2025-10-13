using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class interactuar : MonoBehaviour
{
    public Transform jugador;
    public float distanciaActivacion = 3f;
    public Image iconoFlotante;
    public Image botonE;
    public float tiempoParaActivar = 2f;

    private bool jugadorCerca = false;
    private bool manteniendoE = false;
    private float contadorTiempo = 0f;

    void Update()
    {
        float distancia = Vector3.Distance(jugador.position, transform.position);

        // Mostrar solo el icono flotante si está lejos
        iconoFlotante.gameObject.SetActive(true);

        if (distancia <= distanciaActivacion)
        {
            jugadorCerca = true;
            botonE.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                manteniendoE = true;
                contadorTiempo += Time.deltaTime;

                if (contadorTiempo >= tiempoParaActivar)
                {
                    AccionCompletada();
                    contadorTiempo = 0f;
                }
            }
            else
            {
                manteniendoE = false;
                contadorTiempo = 0f;
            }
        }
        else
        {
            jugadorCerca = false;
            botonE.gameObject.SetActive(false);
            contadorTiempo = 0f;
        }

        // Hacer que el canvas mire siempre al jugador
        transform.GetChild(0).LookAt(jugador);
    }

    void AccionCompletada()
    {
        Debug.Log(" Acción completada: mantuviste E por suficiente tiempo");
        // Aquí puedes poner lo que pase al completar la acción
        // Ejemplo: abrir puerta, recoger objeto, etc.
    }
}
