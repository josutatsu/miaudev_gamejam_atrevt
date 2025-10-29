using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class SplashScreenController : MonoBehaviour
{
    private bool hasPressed = false; // Evita cargar varias veces la escena

    void Update()
    {
        if (hasPressed) return;

        // Detecta si se presiona cualquier tecla, botón o clic del nuevo Input System
        if (Keyboard.current.anyKey.wasPressedThisFrame ||
            Mouse.current.leftButton.wasPressedThisFrame ||
            Mouse.current.rightButton.wasPressedThisFrame)
        {
            hasPressed = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
