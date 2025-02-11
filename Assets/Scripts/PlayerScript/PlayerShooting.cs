using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;   // Assign your bullet prefab in the Inspector
    public Transform firePoint;       // A child transform positioned at the muzzle of your weapon
    public float fireRate = 0.2f;       // Time delay between shots (in seconds)

    private float nextFireTime = 0f;  // Tracks when the player can fire next

    void Update()
    {
        // For mobile or desktop, adjust input accordingly.
        // Here, we use left mouse button as an example.
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the firePoint position.
        // Use the player's current rotation (which is updated by RotateToMouse) so the bullet
        // travels in the direction the player is facing.
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }
}
