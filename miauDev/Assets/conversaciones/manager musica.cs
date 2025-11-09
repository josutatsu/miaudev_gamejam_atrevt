using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Zonas musicales activas")]
    public List<MusicZone> zonasRegistradas = new List<MusicZone>();

    private MusicZone zonaActual;

    void Awake()
    {
        // Singleton simple
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Registrar zonas al iniciar
    public void RegistrarZona(MusicZone zona)
    {
        if (!zonasRegistradas.Contains(zona))
            zonasRegistradas.Add(zona);
    }

    // Activar una zona (solo su música)
    public void ActivarZona(MusicZone nuevaZona)
    {
        // Si ya hay una música sonando, detenerla
        if (zonaActual != null && zonaActual != nuevaZona)
        {
            zonaActual.DetenerMusica();
        }

        // Activar la nueva
        zonaActual = nuevaZona;
        zonaActual.ReproducirMusica();
    }

    // Desactivar cuando el jugador sale
    public void DesactivarZona(MusicZone zona)
    {
        if (zonaActual == zona)
        {
            zonaActual.DetenerMusica();
            zonaActual = null;
        }
    }
}
