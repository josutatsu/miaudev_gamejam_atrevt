using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MusicZone : MonoBehaviour
{
    [Header("Configuración de la música")]
    public AudioClip clipMusica;
    [Range(0f, 1f)] public float volumen = 1f;
    public bool loop = true;
    public string tagJugador = "Player";

    private AudioSource audioSource;

    void Start()
    {
        // Crear o usar un AudioSource en la zona
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clipMusica;
        audioSource.volume = volumen;
        audioSource.loop = loop;

        // Registrar esta zona en el manager
        if (MusicManager.Instance != null)
            MusicManager.Instance.RegistrarZona(this);
    }

    public void ReproducirMusica()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    public void DetenerMusica()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagJugador))
            MusicManager.Instance.ActivarZona(this);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagJugador))
            MusicManager.Instance.DesactivarZona(this);
    }
}
