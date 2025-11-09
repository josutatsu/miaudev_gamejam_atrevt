using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [Header("Lista de objetos a recolectar")]
    public List<GameObject> itemsInScene; // Prefabs que están en la escena
    public int totalItems = 5;

    [Header("UI")]
    public Text uiText; // Texto que mostrará los objetos recolectados

    [Header("Cámara y puerta")]
    public Camera mainCamera;      // Cámara principal
    public Camera doorCamera;      // Cámara que muestra la puerta
    public GameObject puerta;      // La puerta que descenderá
    public float velocidadPuerta = 2f; // Velocidad de descenso
    public float tiempoCamara = 6f;   // Duración de la escena

    private int collectedItems = 0;
    private bool puertaActivada = false;

    void Start()
    {
        UpdateUI();

        if (doorCamera != null)
            doorCamera.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (uiText != null)
            uiText.text = collectedItems + "/" + totalItems + " objetos encontrados";
    }

    public void CollectItem(GameObject item)
    {
        if (itemsInScene.Contains(item))
        {
            collectedItems++;
            itemsInScene.Remove(item);
            Destroy(item);
            UpdateUI();

            if (collectedItems >= totalItems && !puertaActivada)
            {
                StartCoroutine(ActivarPuertaYCamara());
            }
        }
    }

    IEnumerator ActivarPuertaYCamara()
    {
        puertaActivada = true;

        // Activar cámara secundaria
        if (doorCamera != null)
        {
            doorCamera.gameObject.SetActive(true);
            if (mainCamera != null)
                mainCamera.gameObject.SetActive(false);
        }

        // Animar puerta descendiendo durante tiempoCamara segundos
        float elapsedTime = 0f;
        Vector3 startPos = puerta.transform.position;
        Vector3 endPos = startPos + Vector3.down * 20f; // Baja 5 unidades (ajustable)

        while (elapsedTime < tiempoCamara)
        {
            float t = elapsedTime / tiempoCamara;
            puerta.transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar posición final
        puerta.transform.position = endPos;

        // Volver a cámara principal
        if (mainCamera != null)
            mainCamera.gameObject.SetActive(true);

        if (doorCamera != null)
            doorCamera.gameObject.SetActive(false);
    }
}
