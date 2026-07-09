using UnityEngine;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    private Animator anim;
    private int lvlToLoad;

    void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.RegisterFader(this);
    }

    public void FadeToLevel(int lvl)
    {
        lvlToLoad = lvl;
        anim.SetTrigger("FadeOut");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(lvlToLoad);
    }

    private void Restart()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartLevel()
    {
        Invoke("Restart", 2f);
    }

    public void LoadSaveLevel()
    {
        lvlToLoad = PlayerPrefs.GetInt("SavedLevel", 1);
        anim.SetTrigger("FadeOut");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}