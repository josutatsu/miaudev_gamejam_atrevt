using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    [Header("Etiqueta del jugador")]
    public string tagJugador = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
        {
            // Verifica que el nombre no esté vacío antes de cargar
            if (!string.IsNullOrEmpty(nombreEscena))
            {
                SceneManager.LoadScene(nombreEscena);
            }
            else
            {
                Debug.LogWarning("No se ha asignado un nombre de escena en el inspector.");
            }
        }
    }
}
