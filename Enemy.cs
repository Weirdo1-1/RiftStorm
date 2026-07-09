using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base Stats")]
    public float health;

    protected Animator anim;
    protected Rigidbody2D rb;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HurtSequence();

        if (health <= 0)
            DeathSequence();
    }

    public virtual void HurtSequence() { }
    public virtual void DeathSequence() { }
}