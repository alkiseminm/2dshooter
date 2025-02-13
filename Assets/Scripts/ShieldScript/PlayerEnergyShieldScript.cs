using UnityEngine;

public class PlayerEnergyShieldScript : MonoBehaviour
{
    [Header("Shield Prefab")]
    public GameObject shieldPrefab;  // Assign your "Shield" prefab in the Inspector.

    private GameObject activeShield;

    [Header("Shield Settings")]
    public float maxDurability = 100f;
    public float regenDelay = 3.0f;
    public float regenRate = 5.0f;

    [Header("Current Shield Status")]
    public float currentDurability;
    private float timeSinceDamage;

    void Start()
    {
        if (shieldPrefab != null)
        {
            // Instantiate the shield prefab at the player's position and parent it to the player.
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Shield prefab is not assigned!");
        }

        currentDurability = maxDurability;
        timeSinceDamage = regenDelay;
    }

    void Update()
    {
        timeSinceDamage += Time.deltaTime;

        if (timeSinceDamage >= regenDelay && currentDurability < maxDurability)
        {
            RegenerateShield();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (currentDurability > 0)
        {
            timeSinceDamage = 0f;
            currentDurability -= damageAmount;

            if (currentDurability <= 0)
            {
                currentDurability = 0;
                ShieldBreak();
            }
        }
    }

    private void RegenerateShield()
    {
        currentDurability += regenRate * Time.deltaTime;
        if (currentDurability > maxDurability)
        {
            currentDurability = maxDurability;
        }
    }

    private void ShieldBreak()
    {
        Debug.Log("Shield has broken!");
        if (activeShield != null)
        {
            activeShield.SetActive(false);
        }
    }
}
