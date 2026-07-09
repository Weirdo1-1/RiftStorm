using System.Collections;
using UnityEngine;

public class BossSlime : Enemy
{
    [Header("Movement & Combat")]
    public float speed = 3f;
    public float chaseRange = 6f;
    public float attackRange = 3f;
    public float damage = 10f;

    [Header("Knockback Settings")]
    public float forceX = 7f;
    public float forceY = 3f;
    public float duration = 0.2f;

    private Transform player;
    private bool isAttacking = false;
    private bool isDead = false;
    [SerializeField] GameObject portal;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    private void FixedUpdate()
    {
        if (isDead || isAttacking || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            isAttacking = true; // set BEFORE starting coroutine
            StartCoroutine(AttackRoutine());
        }
        else if (distance <= chaseRange)
        {
            MoveToPlayer();
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isWalking", false);
        }
    }

    void MoveToPlayer()
    {
        float dir = player.position.x > transform.position.x ? 1 : -1;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        transform.localScale = new Vector3(dir, 1, 1);
        anim.SetBool("isWalking", true);
    }

    IEnumerator AttackRoutine()
    {
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange + 0.5f)
        {
            player.GetComponentInChildren<PlayerStats>().TakeDamage(damage);
            PlayerMoveControls pMove = player.GetComponent<PlayerMoveControls>();
            pMove.StartCoroutine(pMove.KnockBack(forceX, forceY, duration, transform));
        }

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    public override void HurtSequence()
    {
        anim.SetTrigger("Hurt");
    }

    public override void DeathSequence()
    {
        if (isDead) return;
        isDead = true;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponentInChildren<PolygonCollider2D>().enabled = false;
        anim.SetTrigger("Death");
        portal.SetActive(true);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;
        speed = 0;
    }
}