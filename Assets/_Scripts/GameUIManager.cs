using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    [Header("Victory UI")]
    public GameObject victoryBackground;
    public GameObject victoryText;

    private int maxHealth = 5;
    private int currentHealth;
    private int maxDust = 5;
    private int collectedDust = 0;
    private bool wendySaved = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();

        if (victoryBackground != null) victoryBackground.SetActive(false);
        if (victoryText != null) victoryText.SetActive(false);
    }

    public void UpdateHealth(int newHealth, int max)
    {
        maxHealth = max;
        currentHealth = newHealth;
        UpdateUI();
    }

    public void AddFairyDust(int amount)
    {
        if (wendySaved) return;

        collectedDust += amount;
        if (collectedDust >= maxDust)
        {
            collectedDust = maxDust;
            TriggerVictory();
        }

        UpdateUI();
    }

    private void TriggerVictory()
    {
        Debug.Log("🎉 Wendy Saved! Victory Triggered!");
        wendySaved = true;
        ShowVictoryScreen();
    }

    private void ShowVictoryScreen()
    {
        if (victoryBackground != null)
        {
            victoryBackground.SetActive(true);
            Image bg = victoryBackground.GetComponent<Image>();
            bg.color = Color.white;
        }

        if (victoryText != null)
        {
            victoryText.SetActive(true);
            TextMeshProUGUI text = victoryText.GetComponent<TextMeshProUGUI>();
            text.text = "Wendy Saved!";
            text.color = Color.black;
        }

        Time.timeScale = 0f;
    }

    private void UpdateUI()
    {
        statsText.text = $"HP: {currentHealth}/{maxHealth}\nFairy Dust: {collectedDust}/{maxDust}";
    }
}