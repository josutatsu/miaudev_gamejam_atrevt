using System.Collections;
using UnityEngine;

public class DoorDescend : MonoBehaviour
{
    [Header("Configuración del movimiento")]
    public float tiempoEspera = 20f;    // Espera antes de bajar
    public float velocidad = 2f;        // Velocidad de bajada
    public float distanciaBajada = 5f;  // Cuánto baja la puerta (en unidades)

    private bool bajando = false;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    void Start()
    {
        posicionInicial = transform.position;
        posicionFinal = posicionInicial + Vector3.down * distanciaBajada;

        // Iniciar la cuenta regresiva
        StartCoroutine(EsperarYBajar());
    }

    IEnumerator EsperarYBajar()
    {
        // Espera 20 segundos
        yield return new WaitForSeconds(tiempoEspera);
        bajando = true;

        // Empieza a bajar suavemente
        float progreso = 0f;
        while (progreso < 1f)
        {
            transform.position = Vector3.Lerp(posicionInicial, posicionFinal, progreso);
            progreso += Time.deltaTime * (velocidad / distanciaBajada);
            yield return null;
        }

        // Asegurar posición final exacta
        transform.position = posicionFinal;
        bajando = false;
    }
}
