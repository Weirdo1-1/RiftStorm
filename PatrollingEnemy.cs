using UnityEngine;

public class PatrollingEnemy : Enemy
{
    [Header("Movement Settings")]
    public float speed;
    public int direction = -1;

    [Header("Detection Settings")]
    public Transform groundCheck;
    public Transform WallCheck;
    public LayerMask LayerToCheck;
    public float radius;

    private bool detectGround;
    private bool detectWall;
    private bool isdead;

    [Header("Visual Effects")]
    [SerializeField] ParticleSystem damageParticle;

    void Start()
    {
        damageParticle.Stop();
    }

    private void FixedUpdate()
    {
        if (isdead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Flip();
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    public void Flip()
    {
        detectGround = Physics2D.OverlapCircle(groundCheck.position, radius, LayerToCheck);
        detectWall = Physics2D.OverlapCircle(WallCheck.position, radius, LayerToCheck);

        if (detectWall || detectGround == false)
        {
            direction *= -1;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    public override void HurtSequence()
    {
        anim.SetTrigger("Hurt");
        damageParticle.Play();
    }

    public override void DeathSequence()
    {
        if (isdead) return;
        isdead = true;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        anim.SetTrigger("Death");
        rb.gravityScale = 0;
        speed = 0;
        damageParticle.Stop();
        ParticleSystem instance = Instantiate(damageParticle, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, 0.5f);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null && WallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, radius);
            Gizmos.DrawWireSphere(WallCheck.position, radius);
        }
    }
}