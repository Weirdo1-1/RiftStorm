using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stats"))
        {
            PlayerStats playerStats = collision.GetComponentInChildren<PlayerStats>();

            if (playerStats.health == playerStats.maxHealth) return;

            playerStats.IncreaseHealth(heal);
            Destroy(gameObject);
        }
    }
}