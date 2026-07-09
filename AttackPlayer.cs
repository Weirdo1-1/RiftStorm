using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public float attackDamage;
    private int enemyLayer;

    void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
            else
            {
            }
        }
    }
}