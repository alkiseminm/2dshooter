using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;          // Maximum stamina value
    public float currentStamina = 100f;      // Current stamina value
    public float staminaDrainRate = 20f;     // How fast stamina drains per second while sprinting
    public float staminaRegenRate = 15f;     // How fast stamina regenerates per second
    public float regenDelay = 1.5f;          // Time to wait before stamina starts regenerating

    // Internal timer to track how long since sprinting stopped.
    [SerializeField] private float regenTimer = 0f;

    // Reference to the PlayerController script.
    private PlayerMovement playerController;

    void Start()
    {
        // Assuming the PlayerController is attached to the same GameObject.
        playerController = GetComponent<PlayerMovement>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on the GameObject!");
        }
    }

    void Update()
    {
        //Debug.Log(currentStamina);
        
        // Only run stamina logic if we successfully found the PlayerController.
        if (playerController != null)
        {
            // Check if the player is sprinting.
            if (playerController.currentState == PlayerMovement.PlayerState.Sprinting)
            {
                // Drain stamina over time.
                currentStamina -= staminaDrainRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

                // Reset the regeneration timer because the player is still sprinting.
                regenTimer = 0f;
            }
            else
            {
                // Not sprinting: increase the timer.
                regenTimer += Time.deltaTime;
                // Once the delay has passed, start regenerating stamina.
                if (regenTimer >= regenDelay)
                {
                    currentStamina += staminaRegenRate * Time.deltaTime;
                    currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
                }
            }
        }
    }
}
