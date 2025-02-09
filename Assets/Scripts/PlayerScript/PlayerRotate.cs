using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    void Update()
    {
        // Get the current mouse position
        Vector3 mousePos = Input.mousePosition;
        // Set the z coordinate to the distance between the camera and the player.
        // (Assuming your camera is behind the player along the z-axis)
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        // Convert the adjusted mouse position to world space
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calculate the direction vector from the player to the mouse position
        Vector2 direction = new Vector2(worldMousePos.x - transform.position.x,
                                        worldMousePos.y - transform.position.y);

        // Rotate the player so that its "up" vector points toward the mouse position
        transform.up = direction;
    }
}
