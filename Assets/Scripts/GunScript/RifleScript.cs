using UnityEngine;
using System.Collections;

public class ThreeRoundBurstRifle : MonoBehaviour, IWeapon
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    [Header("Burst Settings")]
    public float burstDelay = 0.1f;
    public int bulletsPerBurst = 3;

    [Header("Weapon Settings")]
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("Fire Point")]
    public Transform firePoint;

    private bool isBurstFiring = false;

    public void Shoot(Vector2 direction)
    {
        if (Time.time >= nextFireTime && !isBurstFiring)
        {
            nextFireTime = Time.time + fireRate;
            StartCoroutine(BurstFire(direction));
        }
    }

    private IEnumerator BurstFire(Vector2 direction)
    {
        isBurstFiring = true;

        for (int i = 0; i < bulletsPerBurst; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Optionally set the bullet's velocity if needed:
            // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // if (rb != null)
            // {
            //     rb.velocity = direction * bulletSpeed;
            // }
            if (i < bulletsPerBurst - 1)
            {
                yield return new WaitForSeconds(burstDelay);
            }
        }

        isBurstFiring = false;
    }
}
