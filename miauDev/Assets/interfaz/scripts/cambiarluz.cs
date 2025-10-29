using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cambiarluz : MonoBehaviour
{
    [Header("Color en español: rojo, azul, negro, lila, amarillo, normal")]
    public string colorSeleccionado = "normal";

    private Light luz;

    void Awake()
    {
        luz = GetComponent<Light>();
        if (luz == null)
        {
            Debug.LogWarning("Este GameObject no tiene un componente Light.");
        }
    }

    public void AplicarColor()
    {
        if (luz == null) return;

        switch (colorSeleccionado.ToLower())
        {
            case "rojo":
                luz.color = Color.red;
                break;
            case "azul":
                luz.color = Color.blue;
                break;
            case "negro":
                luz.color = Color.black;
                break;
            case "lila":
                luz.color = new Color(0.6f, 0.4f, 0.8f);
                break;
            case "amarillo":
                luz.color = Color.yellow;
                break;
            case "normal":
                luz.color = Color.white;
                break;
            default:
                Debug.LogWarning("Color no reconocido. Usa: rojo, azul, negro, lila, amarillo, normal.");
                break;
        }
    }


}
