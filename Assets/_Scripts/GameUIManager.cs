using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    private int maxHealth = 5;
    private int currentHealth;
    private int maxDust = 5;
    private int collectedDust;

    void Start()
    {
        currentHealth = maxHealth;
        collectedDust = 0;
        UpdateUI();
    }

    public void UpdateHealth(int newHealth, int max)
    {
        maxHealth = max;
        currentHealth = newHealth;
        UpdateUI();
    }

    // Ensure only one Fairy Dust adds per collection
    public void AddDust(int amount)
    {
        // Only add once, prevent duplicate increments
        if (collectedDust < maxDust)
        {
            collectedDust += 1;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        statsText.text = $"HP: {currentHealth}/{maxHealth}\nFairy Dust: {collectedDust}/{maxDust}";
    }
}