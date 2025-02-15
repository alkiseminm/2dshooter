using UnityEngine;

public class GunBotHealth : MonoBehaviour
{
    public int gunBotMaxHealth = 100;
    [SerializeField] private int gunBotCurrentHealth;

    void Start()
    {
        gunBotCurrentHealth = gunBotMaxHealth;
    }

    /// <summary>
    /// Reduces health by the specified damage amount.
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        gunBotCurrentHealth -= damageAmount;
        if (gunBotCurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Optionally add death effects, animations, loot drops, etc.
        Destroy(gameObject);
    }
}
