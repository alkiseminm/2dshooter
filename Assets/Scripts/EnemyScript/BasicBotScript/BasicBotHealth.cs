using UnityEngine;

public class BasicBotHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Reduces enemy health when taking damage.
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // Uncomment to see debug info:
        // Debug.Log(gameObject.name + " took " + damage + " damage. HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles enemy death.
    /// </summary>
    private void Die()
    {
        // Optionally add death effects, animations, etc.
        Destroy(gameObject);
    }
}
