using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Speed at which the player moves

    private Rigidbody2D rb;     // Reference to the Rigidbody2D component
    private Vector2 movement;   // Movement input vector

    // Called when the script instance is being loaded.
    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Called once per frame for handling input.
    void Update()
    {
        // Read input from the keyboard (default axes: Horizontal and Vertical)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector to ensure consistent speed in all directions (e.g., diagonally)
        movement = movement.normalized;
    }

    // Called at fixed intervals (useful for physics updates)
    void FixedUpdate()
    {
        // Calculate new position based on input, speed, and time, then move the Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
