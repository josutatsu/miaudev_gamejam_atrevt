using UnityEngine;

public class DisableSkybox : MonoBehaviour
{
    void Start()
    {
        RenderSettings.skybox = null; // Quita el material del skybox
        DynamicGI.UpdateEnvironment(); // Actualiza la iluminación global
        Camera.main.clearFlags = CameraClearFlags.SolidColor; // Fondo liso
        Camera.main.backgroundColor = Color.gray; // Fondo negro (puedes cambiarlo)
    }
}
