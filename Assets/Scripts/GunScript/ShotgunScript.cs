using UnityEngine;
using System.Collections;

public class Shotgun : MonoBehaviour, IWeapon
{
    [Header("Pellet Settings")]
    public GameObject pelletPrefab;
    public float pelletSpeed = 10f;

    [Header("Shotgun Settings")]
    public int totalPellets = 7;
    public float fixedAngleOffset = 5f;
    public float randomAngleMax = 10f;
    public float pelletDelay = 0f;

    [Header("Weapon Settings")]
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    [Header("Fire Point")]
    public Transform firePoint;

    private bool isFiring = false;

    public void Shoot(Vector2 direction)
    {
        if (Time.time >= nextFireTime && !isFiring)
        {
            nextFireTime = Time.time + fireRate;
            StartCoroutine(FireShotgun());
        }
    }

    private IEnumerator FireShotgun()
    {
        isFiring = true;

        // Fire the 3 fixed pellets
        FirePellet(0f);
        FirePellet(-fixedAngleOffset);
        FirePellet(fixedAngleOffset);

        // Fire the remaining random pellets
        int randomPellets = totalPellets - 3;
        for (int i = 0; i < randomPellets; i++)
        {
            float randomOffset = Random.Range(-randomAngleMax, randomAngleMax);
            FirePellet(randomOffset);
            if (pelletDelay > 0f)
            {
                yield return new WaitForSeconds(pelletDelay);
            }
        }

        isFiring = false;
        yield return null;
    }

    private void FirePellet(float angleOffset)
    {
        Quaternion pelletRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angleOffset);
        GameObject pellet = Instantiate(pelletPrefab, firePoint.position, pelletRotation);
        Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = pellet.transform.up * pelletSpeed;
        }
        // Optionally update a Bullet script if needed:
        Bullet bulletScript = pellet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.speed = pelletSpeed;
        }
    }
}
