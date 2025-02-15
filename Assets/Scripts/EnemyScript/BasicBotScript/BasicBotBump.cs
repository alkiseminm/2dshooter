using UnityEngine;

public class BasicBotBump : MonoBehaviour, IBumpDamage
{
    [SerializeField] private int damage = 10;

    /// <summary>
    /// Returns the damage amount this enemy deals.
    /// </summary>
    public int Damage => damage;
}
