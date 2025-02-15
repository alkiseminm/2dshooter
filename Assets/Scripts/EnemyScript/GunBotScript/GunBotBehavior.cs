using UnityEngine;

public class GunBotBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float gunBotMoveSpeed = 3f;
    public float gunBotStoppingDistance = 5f;    // Prevents the bot from getting too close to the player.
    public float gunBotMoveDuration = 2f;        // The maximum time the bot moves before shooting.

    [Header("Movement Command")]
    public Vector2 moveDirection = Vector2.zero; // If nonzero, this direction is used for movement.
                                                 // Otherwise, the bot will move toward the player.

    [Header("Shooting Settings")]
    public GameObject gunBotBulletPrefab;
    public Transform gunBotBulletSpawnPoint;
    public float gunBotBulletInterval = 0.3f;      // Time between each bullet in a burst.
    public int gunBotBulletsPerBurst = 5;          // Number of bullets per burst.
    public float gunBotBurstCooldown = 2f;         // Cooldown after a burst.

    private Transform playerTransform;

    private enum GunBotState { Moving, Shooting, Cooldown }
    private GunBotState currentState = GunBotState.Moving;

    // Timers and counters
    private float moveTimer = 0f;
    private int bulletsShot = 0;
    private float bulletTimer = 0f;
    private float cooldownTimer = 0f;

    void Start()
    {
        // Find the player by tag.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure your Player GameObject has the 'Player' tag.");
        }

        // If no bullet spawn point is assigned, default to this transform.
        if (gunBotBulletSpawnPoint == null)
        {
            gunBotBulletSpawnPoint = transform;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case GunBotState.Moving:
                moveTimer += Time.deltaTime;

                // If the bot gets too close to the player, immediately transition to shooting.
                if (distanceToPlayer <= gunBotStoppingDistance)
                {
                    currentState = GunBotState.Shooting;
                    bulletsShot = 0;
                    bulletTimer = 0f;
                    break;
                }

                // After moving for the full duration, transition to shooting regardless of distance.
                if (moveTimer >= gunBotMoveDuration)
                {
                    currentState = GunBotState.Shooting;
                    bulletsShot = 0;
                    bulletTimer = 0f;
                    break;
                }

                // Move in the commanded direction if provided, else move toward the player.
                Vector2 direction = moveDirection != Vector2.zero ? moveDirection.normalized :
                                    (playerTransform.position - transform.position).normalized;
                transform.position += (Vector3)(direction * gunBotMoveSpeed * Time.deltaTime);
                break;

            case GunBotState.Shooting:
                bulletTimer += Time.deltaTime;
                if (bulletTimer >= gunBotBulletInterval)
                {
                    ShootGunBotBullet();
                    bulletsShot++;
                    bulletTimer = 0f;

                    // After firing the designated burst, switch to cooldown.
                    if (bulletsShot >= gunBotBulletsPerBurst)
                    {
                        currentState = GunBotState.Cooldown;
                        cooldownTimer = gunBotBurstCooldown;
                    }
                }
                break;

            case GunBotState.Cooldown:
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0f)
                {
                    // Reset movement timer and resume moving.
                    currentState = GunBotState.Moving;
                    moveTimer = 0f;
                }
                break;
        }
    }

    private void ShootGunBotBullet()
    {
        // Instantiate the bullet and set its direction toward the player.
        GameObject bullet = Instantiate(gunBotBulletPrefab, gunBotBulletSpawnPoint.position, Quaternion.identity);
        GunBotBullet bulletScript = bullet.GetComponent<GunBotBullet>();
        if (bulletScript != null)
        {
            Vector2 shootDirection = (playerTransform.position - gunBotBulletSpawnPoint.position).normalized;
            bulletScript.SetDirection(shootDirection);
        }
        else
        {
            Debug.LogWarning("The bullet prefab is missing a GunBotBullet component!");
        }
    }
}
