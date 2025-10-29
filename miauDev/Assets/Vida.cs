using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Vida del objeto")]
    public float maxHealth = 100f;   // Vida máxima
    private float currentHealth;     // Vida actual

    void Start()
    {
        currentHealth = maxHealth;   // Al iniciar, llena la vida
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + " recibió daño. Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para curarse
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(gameObject.name + " se curó. Vida actual: " + currentHealth);
    }

    // Qué pasa al morir
    void Die()
    {
        Debug.Log(gameObject.name + " ha muerto.");
        Destroy(gameObject); // destruye el objeto al morir
    }
}
