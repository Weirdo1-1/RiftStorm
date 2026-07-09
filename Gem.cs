using UnityEngine;

public class Gem : MonoBehaviour
{
    [SerializeField] ParticleSystem gemParticle;

    void Start()
    {
        GameManager.RegisterGem(this);
        gemParticle.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollectibles>().GemCollected();
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            ParticleSystem instance = Instantiate(gemParticle, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, 0.5f);
            GameManager.RemoveGemFromList(this);
        }
    }
}