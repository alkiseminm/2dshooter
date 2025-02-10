using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.0f;  // Movement speed of the enemy

    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Assuming your player has the tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure your player GameObject has the tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate direction towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position,
                                                       playerTransform.position,
                                                       moveSpeed * Time.deltaTime);
        }
    }
}
