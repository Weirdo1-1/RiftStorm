using System.Collections;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private GatherInput gI;
    private Rigidbody2D rb;
    private Animator anim;
    private int direction = 1;
    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool grounded = true;
    public bool knockbacked = false;
    public bool hasControl = true;

    void Start()
    {
        gI = GetComponent<GatherInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Animate();
    }

    private void FixedUpdate()
    {
        CheckStatus();
        if (knockbacked || hasControl == false) return;
        Move();
        JumpPlayer();
    }

    private void Move()
    {
        Flip();
        rb.linearVelocity = new Vector2(speed * gI.valueX, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (gI.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }

    private void JumpPlayer()
    {
        if (gI.jumpInput && grounded)
        {
            rb.linearVelocity = new Vector2(gI.valueX * speed, jumpForce);
        }
        gI.jumpInput = false;
    }

    private void CheckStatus()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftHit || rightHit;
        SeeRay(leftHit, rightHit);
    }

    private void SeeRay(RaycastHit2D leftHit, RaycastHit2D rightHit)
    {
        Color colorLeft = leftHit ? Color.green : Color.red;
        Color colorRight = rightHit ? Color.green : Color.red;
        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, colorLeft);
        Debug.DrawRay(rightPoint.position, Vector2.down * rayLength, colorRight);
    }

    private void Animate()
    {
        if (!hasControl) return;
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("vSpeed", rb.linearVelocity.y);
    }

    public void CancelKnockback()
    {
        StopAllCoroutines();
        knockbacked = false;
        rb.linearVelocity = Vector2.zero;
    }

    public IEnumerator KnockBack(float forceX, float forceY, float duration, Transform otherObject)
    {
        int knockbackDirection = (transform.position.x < otherObject.position.x) ? -1 : 1;
        knockbacked = true;
        rb.linearVelocity = Vector2.zero;
        Vector2 theForce = new Vector2(knockbackDirection * forceX, forceY);
        rb.AddForce(theForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        knockbacked = false;
        rb.linearVelocity = Vector2.zero;
    }
}