using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    void Update()
    {
        // Detecta cualquier tecla o clic del mouse
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
