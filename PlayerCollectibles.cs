using TMPro;
using UnityEngine;

public class PlayerCollectibles : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    public int gemNuber;

    void Start()
    {
        gemNuber = PlayerPrefs.GetInt("PlayerGemKey", 0);
        textComponent = GameObject.FindGameObjectWithTag("GemUI")
            .GetComponentInChildren<TextMeshProUGUI>();
        UpdateText();
    }

    private void UpdateText()
    {
        textComponent.text = gemNuber.ToString();
    }

    public void GemCollected()
    {
        gemNuber++;
        UpdateText();
    }
}