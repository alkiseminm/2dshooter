using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Damage the player takes upon collision with an enemy.
    [SerializeField] private int enemyDamage = 10;
    // Knockback force magnitude.
    [SerializeField] private float knockbackForce = 10f;

    // References to the player's health script and controller.
    private PlayerHealth playerHealth;
    private PlayerController playerController;

    void Start()
    {
        // Get the PlayerHealth and PlayerController components.
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component not found on the player!");
        }

        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the player!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is tagged as "Enemy".
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage to the player.
            playerHealth.TakeDamage(enemyDamage);

            // Calculate knockback direction (from enemy to player).
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply knockback by calling the method on the PlayerController.
            playerController.AddKnockback(knockbackDirection * knockbackForce);

            // Optionally, apply an equal and opposite knockback to the enemy if it has a Rigidbody2D.
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.linearVelocity = -knockbackDirection * knockbackForce;
            }
        }
    }
}
