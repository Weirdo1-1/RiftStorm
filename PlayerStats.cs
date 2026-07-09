using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] CapsuleCollider2D deadCollider;

    public float maxHealth = 100f;
    public float health;
    private PlayerMoveControls playerMove;
    private PlayerAttckControls pAC;
    public bool canTakeDamage = true;
    public Image healthBarUI;

    public GameObject faceNormal;
    public GameObject faceDamage;
    public GameObject faceDead;

    void Start()
    {
        playerMove = GetComponentInParent<PlayerMoveControls>();
        pAC = GetComponentInParent<PlayerAttckControls>();
        health = PlayerPrefs.GetFloat("PlayerHealthKey", maxHealth);
        HealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            anim.SetBool("Hurt", true);
            faceDamage.SetActive(true);
            faceDead.SetActive(false);
            faceNormal.SetActive(false);
            playerMove.hasControl = false;
            HealthUI();
            pAC.ResetAttack();

            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                playerMove.CancelKnockback();
                faceDamage.SetActive(false);
                faceDead.SetActive(true);
                faceNormal.SetActive(false);
                Debug.Log("Player is dead");
                PlayerPrefs.SetFloat("PlayerHealthKey", maxHealth);
                GameManager.ManagerRestartLevel();
            }

            StartCoroutine(DamagePrevention());
        }
    }

    public IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.20f);

        playerMove.hasControl = true;

        if (health > 0)
        {
            canTakeDamage = true;
            anim.SetBool("Hurt", false);
            faceDamage.SetActive(false);
            faceDead.SetActive(false);
            faceNormal.SetActive(true);
        }
        else
        {
            playerMove.hasControl = false;
            anim.SetBool("Dead", true);
            deadCollider.size = new Vector2(deadCollider.size.x, 0.1f);
        }
    }

    public void IncreaseHealth(float heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        HealthUI();
    }

    public void HealthUI()
    {
        healthBarUI.fillAmount = health / maxHealth;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerHealthKey");
    }
}