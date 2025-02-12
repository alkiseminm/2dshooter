using UnityEngine;

public interface IWeapon
{
    /// <summary>
    /// Fires the weapon in the given direction.
    /// Each weapon will handle its own cooldown and firing behavior.
    /// </summary>
    /// <param name="direction">Normalized direction to shoot.</param>
    void Shoot(Vector2 direction);
}
