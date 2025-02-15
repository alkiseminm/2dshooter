using UnityEngine;

public class GunBotRotate : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // Find the player in the scene by its tag.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your Player GameObject has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the GunBot to the player.
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            // Rotate the GunBot so its 'up' vector points toward the player.
            transform.up = direction;
        }
    }
}
