using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Fader fader;
    private List<Gem> gems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gems = new List<Gem>();
    }

    public static void RegisterFader(Fader f)
    {
        if (Instance == null) return;
        Instance.fader = f;
    }

    public static void ManagerLoadLevel(int index)
    {
        if (Instance == null) return;
        Instance.fader.FadeToLevel(index);
    }

    public static void ManagerRestartLevel()
    {
        if (Instance == null) return;
        Instance.gems.Clear();
        Instance.fader.RestartLevel();
    }

    public static void RegisterGem(Gem gem)
    {
        if (Instance == null) return;
        if (!Instance.gems.Contains(gem))
            Instance.gems.Add(gem);
    }

    public static void RemoveGemFromList(Gem gem)
    {
        if (Instance == null) return;
        Instance.gems.Remove(gem);
    }
}