using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState { Standing, Walking, Sprinting }
    public PlayerState currentState;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;

    private Rigidbody2D rb;
    private Vector2 movement; // Raw player input (doesn't include knockback)

    // Reference to the stamina system.
    private StaminaSystem staminaSystem;

    [Header("Knockback Settings")]
    private Vector2 knockbackVelocity = Vector2.zero;
    [SerializeField] private float knockbackDecay = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        staminaSystem = GetComponent<StaminaSystem>();
        if (staminaSystem == null)
        {
            Debug.LogError("StaminaSystem component not found on the GameObject!");
        }
    }

    void Update()
    {
        // Read movement input from the keyboard (Horizontal and Vertical axes).
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize input to maintain consistent speed in all directions.
        movement = movement.normalized;

        // Update the player's state based on input and stamina.
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

    void FixedUpdate()
    {
        float currentSpeed = moveSpeed;
        if (currentState == PlayerState.Sprinting)
        {
            currentSpeed *= sprintMultiplier;
        }

        // Player's intentional movement velocity (without knockback).
        Vector2 inputVelocity = movement * currentSpeed;

        // Apply both movement and knockback.
        rb.linearVelocity = inputVelocity + knockbackVelocity;

        // Gradually decay the knockback effect.
        knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, knockbackDecay * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Adds knockback to the player by setting the knockback velocity.
    /// </summary>
    public void AddKnockback(Vector2 knockback)
    {
        knockbackVelocity = knockback;
    }

    /// <summary>
    /// Expose raw player input movement (used by RotateToMouse for correct rotation).
    /// </summary>
    public Vector2 InputMovement
    {
        get { return movement; }
    }
}
