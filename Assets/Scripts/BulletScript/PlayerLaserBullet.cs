using UnityEngine;

public class PlayerLaserBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 20f;         // How fast the bullet moves.
    public int damage = 10;           // Damage applied on hit.
    public float lifeTime = 2f;       // Time before the bullet auto-destroys.

    void Start()
    {
        // Destroy this bullet after its lifetime expires.
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move the bullet forward (local up) every frame.
        transform.Translate(Vector2.up * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for any object implementing IDamageable.
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        // Destroy the bullet on impact.
        Destroy(gameObject);
    }
}
