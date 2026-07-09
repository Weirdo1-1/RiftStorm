using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public int lvlToLoad;

    public void LoadLevel()
    {
        SceneManager.LoadScene(lvlToLoad);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Portal touched by: " + collision.gameObject.name);

        if (collision.GetComponentInParent<PlayerMoveControls>() != null)
        {
            Debug.Log("Player entered portal. Target scene index: " + lvlToLoad);

            PlayerStats playerStats = collision.GetComponentInChildren<PlayerStats>();
            if (playerStats != null)
                PlayerPrefs.SetFloat("PlayerHealthKey", playerStats.health);

            PlayerCollectibles playerCollectibles = collision.GetComponentInChildren<PlayerCollectibles>();
            if (playerCollectibles != null)
                PlayerPrefs.SetInt("PlayerGemKey", playerCollectibles.gemNuber);

            GameManager.ManagerLoadLevel(lvlToLoad);
        }
    }
}