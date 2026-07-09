using UnityEngine;

public class AudioToggle : MonoBehaviour
{
    private AudioSource music;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    public void ToggleMusic(bool isOn)
    {
        music.mute = !isOn;
    }
}