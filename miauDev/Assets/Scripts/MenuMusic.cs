using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // Evita duplicados si recargas la escena
        if (FindObjectsOfType<MenuMusic>().Length > 1)
            Destroy(gameObject);
    }
}
