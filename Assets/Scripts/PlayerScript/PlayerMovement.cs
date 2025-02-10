using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Define the possible player states.
    public enum PlayerState { Standing, Walking, Sprinting }
    public PlayerState currentState;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;            // Base speed at which the player moves
    public float sprintMultiplier = 1.5f;   // Speed multiplier when sprinting

    private Rigidbody2D rb;     // Reference to the Rigidbody2D component
    private Vector2 movement;   // Movement input vector

    // Reference to the stamina system.
    private StaminaSystem staminaSystem;

    // Called when the script instance is being loaded.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Get the StaminaSystem component (make sure it's attached on the same GameObject).
        staminaSystem = GetComponent<StaminaSystem>();
        if (staminaSystem == null)
        {
            Debug.LogError("StaminaSystem component not found on the GameObject!");
        }
    }

    // Called once per frame for handling input and updating the player's state.
    void Update()
    {
        // Read movement input from the keyboard (Horizontal and Vertical axes).
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector to maintain consistent speed in all directions.
        movement = movement.normalized;

        // Update the player's state based on input and stamina:
        // - Standing if no movement input is provided.
        // - Sprinting if moving, left shift is held, and there is stamina available.
        // - Walking otherwise.
        if (movement == Vector2.zero)
        {
            currentState = PlayerState.Standing;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && staminaSystem != null && staminaSystem.currentStamina > 0)
        {
            currentState = PlayerState.Sprinting;
        }
        else
        {
            currentState = PlayerState.Walking;
        }
    }

    // Called at fixed intervals (useful for physics updates).
    void FixedUpdate()
    {
        // Use the current state to determine speed.
        float currentSpeed = moveSpeed;
        if (currentState == PlayerState.Sprinting)
        {
            currentSpeed *= sprintMultiplier;
        }

        // Move the player by updating the Rigidbody2D's position.
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }
}
