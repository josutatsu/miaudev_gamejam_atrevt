using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Text doorMessage;
    public bool isLocked = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isLocked)
                doorMessage.text = "Necesitas la llave";
            else
            {
                doorMessage.text = "Puerta desbloqueada!";
                SceneManager.LoadScene("goodending");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorMessage.text = "";
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }
}
