using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiardeEscenas : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string nombreEscena;

    // Este m�todo puede ser llamado desde un bot�n en el Inspector
    public void CargarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena.");
        }
    }
}