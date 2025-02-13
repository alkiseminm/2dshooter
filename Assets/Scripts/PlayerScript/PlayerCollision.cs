using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private float knockbackForce = 10f;

    private PlayerDamageHandler playerDamageHandler; // Changed from PlayerHealth
    private PlayerMovement playerController;

    void Start()
    {
        // Get the PlayerDamageHandler component
        playerDamageHandler = GetComponent<PlayerDamageHandler>();
        if (playerDamageHandler == null)
        {
            Debug.LogError("PlayerDamageHandler component not found on the player!");
        }

        playerController = GetComponent<PlayerMovement>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the player!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Use the damage handler to process damage (checks shield first)
            playerDamageHandler.TakeDamage(enemyDamage); // Updated line

            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            playerController.AddKnockback(knockbackDirection * knockbackForce);

            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.linearVelocity = -knockbackDirection * knockbackForce;
            }
        }
    }
}