using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public enum RotationState { Aiming, NotAiming }
    public RotationState currentState;

    // Minimum movement (in world units) required to update rotation.
    public float minMovementThreshold = 0.01f;

    // Reference to the player's PlayerController to access the input movement.
    private PlayerMovement playerController;

    void Start()
    {
        currentState = RotationState.NotAiming;
        playerController = GetComponent<PlayerMovement>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            currentState = RotationState.Aiming;

            // Get mouse position and convert to world coordinates.
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // Calculate the direction from the player to the mouse.
            Vector2 directionToMouse = new Vector2(
                worldMousePos.x - transform.position.x,
                worldMousePos.y - transform.position.y);

            if (directionToMouse.sqrMagnitude > 0.001f)
            {
                transform.up = directionToMouse;
            }
        }
        else
        {
            currentState = RotationState.NotAiming;

            // Use the input movement from the PlayerController (ignoring knockback).
            Vector2 inputMovement = playerController.InputMovement;

            // Only update rotation if there's significant input.
            if (inputMovement.magnitude > minMovementThreshold)
            {
                transform.up = inputMovement;
            }
            // Otherwise, do nothing so that the player maintains its current rotation.
        }
    }
}
