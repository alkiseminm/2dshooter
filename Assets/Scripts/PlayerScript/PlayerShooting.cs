using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Settings")]
    public float fireRate = 0.2f;  // Time delay between trigger pulls (in seconds)

    // Reference to the equipped gun's script (e.g., your ThreeRoundBurstRifle)
    public ThreeRoundBurstRifle equippedGun;

    private float nextFireTime = 0f;  // Tracks when the player can fire next

    void Update()
    {
        // When the left mouse button is held down and enough time has passed...
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            // Calculate the shooting direction based on the mouse position.
            // You can also use transform.up if your RotateToMouse script is handling aiming.
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            // Fire the equipped gun using its own Shoot() method.
            if (equippedGun != null)
            {
                equippedGun.Shoot(shootDirection);
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.LogWarning("No equipped gun assigned in PlayerShooting!");
            }
        }
    }
}
