using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10f;

    private PlayerDamageHandler playerDamageHandler;
    private PlayerMovement playerController;

    void Start()
    {
        playerDamageHandler = GetComponent<PlayerDamageHandler>();
        if (playerDamageHandler == null)
        {
            Debug.LogError("PlayerDamageHandler component not found on the player!");
        }

        playerController = GetComponent<PlayerMovement>();
        if (playerController == null)
        {
            Debug.LogError("PlayerMovement component not found on the player!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IBumpDamage enemyBump = collision.gameObject.GetComponent<IBumpDamage>();
            if (enemyBump != null)
            {
                // Process bump damage
                playerDamageHandler.TakeDamage(enemyBump.Damage);
            }
            else
            {
                Debug.LogWarning("No enemy bump damage component found on enemy: " + collision.gameObject.name);
            }

            // Calculate knockback direction
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            playerController.AddKnockback(knockbackDirection * knockbackForce);

            // Optionally apply knockback to the enemy as well
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                enemyRb.linearVelocity = -knockbackDirection * knockbackForce;
            }
        }
    }
}
