using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    public PlayerHealth playerHealth; // Ensure this is assigned (e.g., attached to the same player)
    private PlayerEnergyShieldScript energyShield;

    void Start()
    {
        // If EnergyShieldScript is attached to the same GameObject or its children, this will find it.
        energyShield = GetComponentInChildren<PlayerEnergyShieldScript>();

        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerHealth>();
        }
    }

    /// <summary>
    /// Applies damage: first to the shield if it has durability, otherwise to player health.
    /// </summary>
    public void TakeDamage(int damageAmount)
    {
        // Check if shield is active and has durability left.
        if (energyShield != null && energyShield.currentDurability > 0)
        {
            // Only apply damage to the shield.
            energyShield.TakeDamage(damageAmount);
        }
        else
        {
            // No shield or shield is broken, apply damage to player health.
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
