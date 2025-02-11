using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject riflePrefab;  // Assign your rifle prefab in the Inspector

    [Header("Weapon Positioning")]
    public Vector3 rifleLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 rifleLocalRotationEuler = Vector3.zero;

    // Reference to the instantiated rifle
    private GameObject equippedRifle;

    private PlayerShooting playerShooting;

    void Start()
    {
        playerShooting = GetComponent<PlayerShooting>();

        if (riflePrefab != null)
        {
            // Instantiate the rifle as a child of the player
            equippedRifle = Instantiate(riflePrefab, transform);
            equippedRifle.transform.localPosition = rifleLocalPosition;
            equippedRifle.transform.localRotation = Quaternion.Euler(rifleLocalRotationEuler);

            // Assign the rifle's script to the PlayerShooting script
            ThreeRoundBurstRifle rifleScript = equippedRifle.GetComponent<ThreeRoundBurstRifle>();
            if (rifleScript != null)
            {
                playerShooting.equippedGun = rifleScript;  // Assign the script dynamically
            }
            else
            {
                Debug.LogError("The instantiated rifle does not have a ThreeRoundBurstRifle script attached!");
            }
        }
        else
        {
            Debug.LogError("No rifle prefab assigned in PlayerWeaponHandler!");
        }
    }
}
