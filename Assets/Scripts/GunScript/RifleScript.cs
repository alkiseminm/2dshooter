using UnityEngine;
using System.Collections;

public class ThreeRoundBurstRifle : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;      // Prefab for the bullet
    public float bulletSpeed = 10f;        // Speed at which bullet travels

    [Header("Burst Settings")]
    public float burstDelay = 0.1f;        // Delay between bullets in a burst
    public int bulletsPerBurst = 3;        // Number of bullets per burst

    [Header("Fire Point")]
    public Transform firePoint;          // The position from where the bullets are fired

    // Flag to ensure we don't start a new burst until the current one is finished
    private bool isBurstFiring = false;

    /// <summary>
    /// Public method to initiate the burst fire.
    /// Call this method (e.g., from your player input script) and pass the shooting direction.
    /// </summary>
    /// <param name="direction">The normalized direction in which to fire bullets.</param>
    public void Shoot(Vector2 direction)
    {
        if (!isBurstFiring)
        {
            StartCoroutine(BurstFire(direction));
        }
    }

    /// <summary>
    /// Coroutine that fires a burst of bullets.
    /// </summary>
    /// <param name="direction">The normalized direction for bullet travel.</param>
    /// <returns></returns>
    private IEnumerator BurstFire(Vector2 direction)
    {
        isBurstFiring = true;

        for (int i = 0; i < bulletsPerBurst; i++)
        {
            // Instantiate the bullet using the firePoint's rotation.
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // (Optional) Remove or comment out the manual velocity assignment:
            // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // if (rb != null)
            // {
            //     rb.linearVelocity = direction * bulletSpeed;
            // }

            if (i < bulletsPerBurst - 1)
            {
                yield return new WaitForSeconds(burstDelay);
            }
        }

        isBurstFiring = false;
    }

}
