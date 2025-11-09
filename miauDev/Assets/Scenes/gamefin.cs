using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 15f;
    public Text timerText;
    public AudioSource endSound;
    public GameObject enemy; // El objeto que perseguirá al jugador
    public Transform player;

    private bool timeUp = false;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Tiempo: " + Mathf.Ceil(timeRemaining).ToString();
        }
        else if (!timeUp)
        {
            timeUp = true;
            TimerEnded();
        }
    }

    void TimerEnded()
    {
        // Reproduce sonido
        if (endSound != null) endSound.Play();

        // Activa enemigo para perseguir al jugador
        if (enemy != null && player != null)
        {
            enemy.GetComponent<EnemyChase>().target = player;
        }

        // Después de un delay opcional puedes cambiar de escena automáticamente
        Invoke("GoToBadEnding", 3f); // 3 segundos antes de enviar a badending
    }

    void GoToBadEnding()
    {
        SceneManager.LoadScene("badending");
    }
}
