using UnityEngine;

public class Chaingun : MonoBehaviour, IWeapon
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 15f;
    public int bulletDamage = 5;

    [Header("Weapon Settings")]
    public float fireRate = 0.1f; // High fire rate for full auto
    private float nextFireTime = 0f;

    [Header("Accuracy Settings")]
    public float initialSpreadAngle = 10f;  // Maximum (idle) spread in degrees
    public float minSpreadAngle = 2f;       // Minimum spread when continuously firing
    public float spreadReductionRate = 5f;  // How many degrees are reduced per shot when firing
    public float spreadIncreaseRate = 2f;   // How many degrees per second the spread increases when not firing

    [SerializeField] private float currentSpreadAngle;
    private float lastShotTime = 0f;

    [Header("Fire Point")]
    public Transform firePoint;

    void Start()
    {
        // Start with the maximum inaccuracy.
        currentSpreadAngle = initialSpreadAngle;
    }

    void Update()
    {
        // When not firing, slowly increase the spread (making it more inaccurate)
        // until it reaches the maximum (initialSpreadAngle).
        if (Time.time - lastShotTime >= fireRate)
        {
            currentSpreadAngle = Mathf.Min(initialSpreadAngle, currentSpreadAngle + spreadIncreaseRate * Time.deltaTime);
        }
    }

    /// <summary>
    /// Call this method continuously (while holding fire).
    /// The bullet will always shoot in the direction the gun is facing.
    /// </summary>
    /// <param name="unusedDirection">This parameter is ignored.</param>
    public void Shoot(Vector2 unusedDirection)
    {
        if (Time.time < nextFireTime)
            return;

        nextFireTime = Time.time + fireRate;
        lastShotTime = Time.time;

        // Use the firePoint's up direction as the base direction.
        Vector2 baseDirection = firePoint.up;

        // Apply random deviation based on current spread.
        float deviation = Random.Range(-currentSpreadAngle * 0.5f, currentSpreadAngle * 0.5f);
        Quaternion deviationRotation = Quaternion.AngleAxis(deviation, Vector3.forward);
        Vector2 modifiedDirection = deviationRotation * baseDirection;

        // Instantiate the bullet and set its properties.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.speed = bulletSpeed;
            bulletScript.damage = bulletDamage;
        }
        // Orient the bullet to move in the modified direction.
        bullet.transform.up = modifiedDirection;

        // Decrease the spread immediately when firing.
        currentSpreadAngle = Mathf.Max(minSpreadAngle, currentSpreadAngle - spreadReductionRate * fireRate);
    }
}
