using UnityEngine;

public class EnemyAttck : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage;

    protected PlayerStats playerStats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stats"))
        {
            playerStats = collision.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            AttackSpecial();
        }
    }

    public virtual void AttackSpecial() { }
}