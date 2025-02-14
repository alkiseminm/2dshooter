using UnityEngine;

public class BasicBotDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    /// <summary>
    /// Returns the damage amount this enemy deals.
    /// </summary>
    public int Damage => damage;
}
