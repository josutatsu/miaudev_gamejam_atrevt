using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscenaPorTiempo : MonoBehaviour
{
    [Header("Configuración de tiempo y escena")]
    [Tooltip("Tiempo en segundos antes de cambiar de escena")]
    public float tiempoEspera = 25f;

    [Tooltip("Nombre de la escena a la que se cambiará")]
    public string nombreEscena = "shooter_cap1";

    private void Start()
    {
        // Inicia el cambio de escena después del tiempo indicado
        Invoke("CambiarEscena", tiempoEspera);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
