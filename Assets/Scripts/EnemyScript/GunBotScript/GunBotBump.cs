using UnityEngine;

public class GunBotBump : MonoBehaviour, IBumpDamage
{
    [SerializeField] private int damage = 20; // Adjust this value in the editor for GunBot bump damage.

    /// <summary>
    /// Returns the damage amount this GunBot deals on collision.
    /// </summary>
    public int Damage => damage;
}
