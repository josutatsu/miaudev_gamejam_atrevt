using System.Collections;
using UnityEngine;

public class SlowDoorDescent : MonoBehaviour
{
    [Header("Movimiento de la puerta")]
    public float distanciaBajada = 5f; // Cuánto baja la puerta
    public float duracion = 5f;        // Cuánto tarda en bajar
    public bool activarAutomatico = false; // Si quieres que empiece sola
    private bool bajando = false;

    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    void Start()
    {
        posicionInicial = transform.position;
        posicionFinal = posicionInicial + Vector3.down * distanciaBajada;

        if (activarAutomatico)
            StartCoroutine(BajarPuerta());
    }

    public void ActivarPuerta()
    {
        if (!bajando)
            StartCoroutine(BajarPuerta());
    }

    IEnumerator BajarPuerta()
    {
        bajando = true;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float progreso = tiempo / duracion;
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, progreso);
            tiempo += Time.deltaTime;
            yield return null;
        }

        transform.position = posicionFinal;
        bajando = false;
    }
}
