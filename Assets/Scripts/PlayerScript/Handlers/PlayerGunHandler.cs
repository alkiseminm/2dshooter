using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapon Prefabs")]
    public GameObject riflePrefab;    // Assign your rifle prefab in the Inspector
    public GameObject shotgunPrefab;  // Assign your shotgun prefab in the Inspector
    public GameObject sniperPrefab;   // Assign your sniper prefab in the Inspector
    public GameObject chaingunPrefab; // Assign your chaingun prefab in the Inspector

    [Header("Weapon Positioning")]
    // Rifle positioning
    public Vector3 rifleLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 rifleLocalRotationEuler = Vector3.zero;

    // Shotgun positioning
    public Vector3 shotgunLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 shotgunLocalRotationEuler = Vector3.zero;

    // Sniper positioning
    public Vector3 sniperLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 sniperLocalRotationEuler = Vector3.zero;

    // Chaingun positioning
    public Vector3 chaingunLocalPosition = new Vector3(0.5f, 0f, 0f);
    public Vector3 chaingunLocalRotationEuler = Vector3.zero;

    // Reference to the currently equipped weapon (rifle, shotgun, sniper, or chaingun)
    private GameObject currentWeapon;

    private PlayerShooting playerShooting;

    void Start()
    {
        // Get the PlayerShooting component from the player.
        playerShooting = GetComponent<PlayerShooting>();
    }

    void Update()
    {
        // Press "1" to equip the rifle.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipRifle();
        }
        // Press "2" to equip the shotgun.
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipShotgun();
        }
        // Press "3" to equip the sniper rifle.
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipSniper();
        }
        // Press "4" to equip the chaingun.
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipChaingun();
        }
    }

    /// <summary>
    /// Equips the rifle: destroys any currently equipped weapon, instantiates the rifle,
    /// sets its local position/rotation, and assigns its shooting script to the PlayerShooting component.
    /// </summary>
    void EquipRifle()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (riflePrefab != null)
        {
            currentWeapon = Instantiate(riflePrefab, transform);
            currentWeapon.transform.localPosition = rifleLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(rifleLocalRotationEuler);

            ThreeRoundBurstRifle rifleScript = currentWeapon.GetComponent<ThreeRoundBurstRifle>();
            if (rifleScript != null)
            {
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
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (shotgunPrefab != null)
        {
            currentWeapon = Instantiate(shotgunPrefab, transform);
            currentWeapon.transform.localPosition = shotgunLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(shotgunLocalRotationEuler);

            Shotgun shotgunScript = currentWeapon.GetComponent<Shotgun>();
            if (shotgunScript != null)
            {
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

    /// <summary>
    /// Equips the sniper rifle: destroys any currently equipped weapon, instantiates the sniper,
    /// sets its local position/rotation, and assigns its shooting script to the PlayerShooting component.
    /// </summary>
    void EquipSniper()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (sniperPrefab != null)
        {
            currentWeapon = Instantiate(sniperPrefab, transform);
            currentWeapon.transform.localPosition = sniperLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(sniperLocalRotationEuler);

            SniperRifle sniperScript = currentWeapon.GetComponent<SniperRifle>();
            if (sniperScript != null)
            {
                playerShooting.equippedGun = sniperScript;
            }
            else
            {
                Debug.LogError("The instantiated sniper does not have a SniperRifle script attached!");
            }
        }
        else
        {
            Debug.LogError("No sniper prefab assigned in PlayerWeaponHandler!");
        }
    }

    /// <summary>
    /// Equips the chaingun: destroys any currently equipped weapon, instantiates the chaingun,
    /// sets its local position/rotation, and assigns its shooting script to the PlayerShooting component.
    /// </summary>
    void EquipChaingun()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        if (chaingunPrefab != null)
        {
            currentWeapon = Instantiate(chaingunPrefab, transform);
            currentWeapon.transform.localPosition = chaingunLocalPosition;
            currentWeapon.transform.localRotation = Quaternion.Euler(chaingunLocalRotationEuler);

            Chaingun chaingunScript = currentWeapon.GetComponent<Chaingun>();
            if (chaingunScript != null)
            {
                playerShooting.equippedGun = chaingunScript;
            }
            else
            {
                Debug.LogError("The instantiated chaingun does not have a Chaingun script attached!");
            }
        }
        else
        {
            Debug.LogError("No chaingun prefab assigned in PlayerWeaponHandler!");
        }
    }
}
