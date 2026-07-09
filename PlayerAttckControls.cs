using UnityEngine;

public class PlayerAttckControls : MonoBehaviour
{
    private PlayerMoveControls pMC;
    private GatherInput gI;
    private Animator anim;

    public bool attacStarted = false;
    public PolygonCollider2D polygonCol;

    [SerializeField] GameObject[] trailSword;

    void Start()
    {
        pMC = GetComponent<PlayerMoveControls>();
        gI = GetComponent<GatherInput>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (gI.tryToAttack)
        {
            if (pMC.knockbacked || pMC.hasControl == false) return;
            anim.SetBool("Attack", true);
        }
    }

    public void ActivateAttack()
    {
        for (int i = 0; i < trailSword.Length; i++)
            trailSword[i].SetActive(true);

        polygonCol.enabled = true;
    }

    public void ResetAttack()
    {
        for (int i = 0; i < trailSword.Length; i++)
            trailSword[i].SetActive(false);

        anim.SetBool("Attack", false);
        gI.tryToAttack = false;
        attacStarted = false;
        polygonCol.enabled = false;
    }
}