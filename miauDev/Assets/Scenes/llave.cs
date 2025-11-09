using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Text keyMessage;
    public Door door;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyMessage.text = "Ya tienes la llave, corre a la salida!";
            if (door != null)
                door.UnlockDoor();

            Destroy(gameObject); // Destruye la llave
        }
    }
}
