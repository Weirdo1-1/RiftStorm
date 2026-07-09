using UnityEngine;

public class AttackPatrolling : EnemyAttck
{
    private PlayerMoveControls playerMove;

    [Header("Knockback Settings")]
    public float forceX;
    public float forceY;
    public float time;

    public override void AttackSpecial()
    {
        playerMove = playerStats.GetComponentInParent<PlayerMoveControls>();

        if (playerMove != null)
        {
            StartCoroutine(playerMove.KnockBack(forceX, forceY, time, transform.parent));
        }
    }
}