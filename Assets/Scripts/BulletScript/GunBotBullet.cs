using UnityEngine;

public class GunBotBullet : MonoBehaviour
{
    public float gunBotBulletSpeed = 10f;
    public int gunBotBulletDamage = 10;
    public float gunBotBulletLifeTime = 5f;  // The bullet is destroyed after this time.

    private Vector2 moveDirection;

    void Start()
    {
        Destroy(gameObject, gunBotBulletLifeTime);
    }

    /// <summary>
    /// Sets the moving direction of the bullet.
    /// </summary>
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }

    void Update()
    {
        transform.Translate(moveDirection * gunBotBulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply damage to the player.
            PlayerDamageHandler damageHandler = collision.GetComponent<PlayerDamageHandler>();
            if (damageHandler != null)
            {
                damageHandler.TakeDamage(gunBotBulletDamage);
            }
            Destroy(gameObject);
        }
        // Optionally, handle collision with obstacles or other objects.
    }
}
