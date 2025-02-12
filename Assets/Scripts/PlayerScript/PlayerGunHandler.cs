using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject riflePrefab;   // Assign your rifle prefab in the Inspector
    public GameObject shotgunPrefab; // Assign your shotgun prefab in the Inspector

    [Header("Weapon Positioning")]
    // Position and rotation for the rifle relative to the player
    public Vector3 rifleLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 rifleLocalRotationEuler = Vector3.zero;

    // Position and rotation for the shotgun relative to the player
    public Vector3 shotgunLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 shotgunLocalRotationEuler = Vector3.zero;

    // Reference to the currently equipped weapon (either rifle or shotgun)
    private GameObject currentWeapon;

    private PlayerShooting playerShooting;

    void Start()
    {
        // Get the PlayerShooting component from the player.
        playerShooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        // When the "1" key is pressed, equip the rifle.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipRifle();
        }
        // When the "2" key is pressed, equip the shotgun.
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipShotgun();
        }
    }

    /// <summary>
    /// Equips the rifle: destroys any currently equipped weapon, instantiates the rifle,
    /// sets its local position/rotation, and assigns its shooting script to the PlayerShooting component.
    /// </summary>
    void EquipRifle()
    {
        // Remove any weapon currently equipped.
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (riflePrefab != null)
        {
            // Instantiate the rifle as a child of the player.
            currentWeapon = Instantiate(riflePrefab, transform);
            currentWeapon.transform.localPosition = rifleLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(rifleLocalRotationEuler);

            // Get the rifle's shooting script (assumed to be ThreeRoundBurstRifle)
            ThreeRoundBurstRifle rifleScript = currentWeapon.GetComponent<ThreeRoundBurstRifle>();
            if (rifleScript != null)
            {
                // Assign the rifle's shooting script to the player's shooting component.
                playerShooting.equippedGun = rifleScript;
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

    /// <summary>
    /// Equips the shotgun: destroys any currently equipped weapon, instantiates the shotgun,
    /// sets its local position/rotation, and assigns its shooting script to the PlayerShooting component.
    /// </summary>
    void EquipShotgun()
    {
        // Remove any weapon currently equipped.
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (shotgunPrefab != null)
        {
            // Instantiate the shotgun as a child of the player.
            currentWeapon = Instantiate(shotgunPrefab, transform);
            currentWeapon.transform.localPosition = shotgunLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(shotgunLocalRotationEuler);

            // Get the shotgun's shooting script (assumed to be Shotgun)
            Shotgun shotgunScript = currentWeapon.GetComponent<Shotgun>();
            if (shotgunScript != null)
            {
                // Assign the shotgun's shooting script to the player's shooting component.
                playerShooting.equippedGun = shotgunScript;
            }
            else
            {
                Debug.LogError("The instantiated shotgun does not have a Shotgun script attached!");
            }
        }
        else
        {
            Debug.LogError("No shotgun prefab assigned in PlayerWeaponHandler!");
        }
    }
}
