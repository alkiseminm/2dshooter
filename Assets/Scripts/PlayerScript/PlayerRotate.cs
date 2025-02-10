using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    // Define the rotation states.
    public enum RotationState { Aiming, NotAiming }
    public RotationState currentState;

    // Minimum movement (in world units) required to update rotation based on movement.
    public float minMovementThreshold = 0.01f;

    // Store the player's position from the last frame.
    private Vector3 lastPosition;

    void Start()
    {
        // Initialize lastPosition to the player's starting position.
        lastPosition = transform.position;
        currentState = RotationState.NotAiming;
    }

    void Update()
    {
        //Debug.Log(currentState);
        
        // Check if the right mouse button is held down.
        if (Input.GetMouseButton(1))
        {
            currentState = RotationState.Aiming;

            // Get the current mouse position in screen space.
            Vector3 mousePos = Input.mousePosition;
            // Adjust the z coordinate so that the ScreenToWorldPoint conversion works correctly.
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

            // Convert the screen position to world space.
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Calculate the direction from the player to the mouse cursor.
            Vector2 directionToMouse = new Vector2(
                worldMousePos.x - transform.position.x,
                worldMousePos.y - transform.position.y);

            // Rotate the player if the direction is significant.
            if (directionToMouse.sqrMagnitude > 0.001f)
            {
                transform.up = directionToMouse;
            }
        }
        else
        {
            currentState = RotationState.NotAiming;

            // Calculate the movement vector by comparing current and previous positions.
            Vector2 movement = transform.position - lastPosition;

            // Rotate the player based on movement if the movement exceeds the threshold.
            if (movement.magnitude > minMovementThreshold)
            {
                transform.up = movement;
            }
        }

        // Update lastPosition for the next frame.
        lastPosition = transform.position;
    }
}
