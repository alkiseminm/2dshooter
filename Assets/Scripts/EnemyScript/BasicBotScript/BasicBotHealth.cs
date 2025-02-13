using UnityEngine;

public class BasicBotHealth : MonoBehaviour
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
        //Debug.Log(gameObject.name + " took " + damage + " damage. HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles enemy death (destroy object, play animation, etc.).
    /// </summary>
    private void Die()
    {
        //Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject); // Removes the enemy from the scene
    }
}
