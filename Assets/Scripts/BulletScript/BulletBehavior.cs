using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // This will now be overwritten by the rifle's bulletSpeed.
    public int damage = 10;
    public float lifeTime = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Use the bullet's speed (which is now set by the rifle) for movement.
        rb.linearVelocity = transform.up * speed;
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<BasicBotHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
