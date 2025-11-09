using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayMusicOnCollision : MonoBehaviour
{
    [Header("Configuración de sonido")]
    public AudioClip musica;              // La música a reproducir
    public string tagJugador = "Player";  // Tag del jugador
    [Range(0f, 1f)] public float volumen = 1f;  // Volumen ajustable (0-1)
    public bool loop = false;             // Repetir música en bucle

    private AudioSource audioSource;
    private bool reproducido = false;

    void Start()
    {
        // Configura el AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volumen;
        audioSource.loop = loop;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!reproducido && other.CompareTag(tagJugador))
        {
            audioSource.clip = musica;
            audioSource.volume = volumen; // Garantiza que use el volumen del Inspector
            audioSource.loop = loop;      // Garantiza que use el loop del Inspector
            audioSource.Play();
            reproducido = true;
        }
    }
}
