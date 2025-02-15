using UnityEngine;

public class GunBotHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int gunBotMaxHealth = 100;
    [SerializeField] private int gunBotCurrentHealth;

    void Start()
    {
        gunBotCurrentHealth = gunBotMaxHealth;
    }

    /// <summary>
    /// Reduces health by the specified damage amount.
    /// </summary>
    public void TakeDamage(int damage)
    {
        gunBotCurrentHealth -= damage;
        if (gunBotCurrentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles GunBot death.
    /// </summary>
    private void Die()
    {
        // Optionally add death effects, animations, loot drops, etc.
        Destroy(gameObject);
    }
}
