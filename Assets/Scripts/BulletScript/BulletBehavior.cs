using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Overwritten by the weapon's bulletSpeed.
    public int damage = 10;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Instead of checking just for a specific component,
        // we check for any component that implements IDamageable.
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
