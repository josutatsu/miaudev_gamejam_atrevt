using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Vida del objeto")]
    public float maxHealth = 100f;   // Vida m�xima
    private float currentHealth;     // Vida actual

    void Start()
    {
        currentHealth = maxHealth;   // Al iniciar, llena la vida
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " recibi� da�o. Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para curarse
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + " se cur�. Vida actual: " + currentHealth);
    }

    // Qu� pasa al morir
    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject); // destruye el objeto al morir
    }
}
