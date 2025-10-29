using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("shooter_cap1"); // Cambiar al nombre por la escena del juego!!!!!!!!!!!!
    }

    public void OpenInfo()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        // Para pruebas en el editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
