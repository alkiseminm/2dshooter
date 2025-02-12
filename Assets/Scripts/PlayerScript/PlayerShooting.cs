using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Reference to the equipped weapon's script.
    // Ensure that whichever weapon is equipped implements IWeapon.
    public MonoBehaviour equippedGun; // Alternatively, use "public IWeapon equippedGun;" if you prefer.

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // Calculate the shooting direction. You could use the mouse position or transform.up if already rotated.
            Vector2 shootDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

            if (equippedGun != null)
            {
                IWeapon weapon = equippedGun as IWeapon;
                if (weapon != null)
                {
                    weapon.Shoot(shootDirection);
                }
                else
                {
                    Debug.LogWarning("The equipped gun does not implement IWeapon!");
                }
            }
            else
            {
                Debug.LogWarning("No equipped gun assigned in PlayerShooting!");
            }
        }
    }
}
