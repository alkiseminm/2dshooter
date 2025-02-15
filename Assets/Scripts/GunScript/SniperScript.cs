using UnityEngine;
using System.Collections;

public class SniperRifle : MonoBehaviour, IWeapon
{
    [Header("Weapon Settings")]
    public float fireRate = 1f;       // Delay between shots (sniper rifles are usually slow)
    private float nextFireTime = 0f;  // Tracks when the sniper can fire next

    [Header("Sniper Settings")]
    public float damage = 50f;        // Damage dealt per shot
    public float range = 100f;        // Maximum distance for the hitscan

    [Header("Fire Point")]
    public Transform firePoint;       // The transform from where the hitscan is cast

    [Header("Effects")]
    public LineRenderer lineRenderer; // Optional: A LineRenderer to simulate a tracer

    /// <summary>
    /// Fires the sniper rifle using a hitscan method.
    /// </summary>
    public void Shoot(Vector2 direction)
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;
        FireHitscan();
    }

    /// <summary>
    /// Casts a ray from the firePoint and applies damage if an enemy is hit.
    /// </summary>
    private void FireHitscan()
    {
        // Use the firePoint's up vector as the shooting direction.
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up, range);

        Vector3 hitPosition = firePoint.position + firePoint.up * range; // Default end point

        if (hitInfo)
        {
            hitPosition = hitInfo.point;

            // Get any component implementing IDamageable and apply damage.
            IDamageable enemy = hitInfo.collider.GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)damage);
            }
        }

        // Optional: Draw a tracer using a LineRenderer.
        if (lineRenderer != null)
        {
            StartCoroutine(DrawShotEffect(hitPosition));
        }
    }

    /// <summary>
    /// Temporarily enables the LineRenderer to simulate a tracer effect.
    /// </summary>
    private IEnumerator DrawShotEffect(Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, endPoint);
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;
    }
}
