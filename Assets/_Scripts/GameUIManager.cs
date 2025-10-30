using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Keep only if you still use TMP for the victory text

public class GameUIManager : MonoBehaviour
{
    [Header("Hearts UI")]
    public Transform heartsParent;   // Drag HeartsRow here
    public GameObject heartPrefab;   // Drag HeartIcon prefab here
    public Sprite heartFull;         // Red filled heart sprite
    public Sprite heartEmpty;        // Gray/empty heart sprite

    [Header("Victory UI (Optional)")]
    public GameObject victoryBackground; // Optional background panel
    public GameObject victoryText;       // Optional TMP text

    [Header("Hearts Icon Size")]
    public Vector2 heartIconSize = new Vector2(20f, 20f); // default size

    private int maxHealth = 5;
    private int currentHealth = 5;
    private bool wendySaved = false;

    private readonly List<Image> heartImgs = new();

    void Start()
    {
        // Build hearts when game starts
        RebuildHearts(maxHealth);

        // Hide victory UI at start
        if (victoryBackground != null) victoryBackground.SetActive(false);
        if (victoryText != null) victoryText.SetActive(false);

        RefreshHearts();
    }

    // Called by BossPeter whenever HP changes
    public void UpdateHealth(int newHealth, int max)
    {
        maxHealth = Mathf.Max(1, max);
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);

        // Rebuild hearts if max HP changed
        if (heartImgs.Count != maxHealth) RebuildHearts(maxHealth);

        RefreshHearts();
    }

    // Create hearts under parent
    void RebuildHearts(int count)
    {
        if (heartsParent == null || heartPrefab == null) return;

        foreach (Transform child in heartsParent)
            Destroy(child.gameObject);
        heartImgs.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(heartPrefab, heartsParent);
            Image img = go.GetComponent<Image>();
            if (img == null) continue;

            // Fixed size, not native pixel size
            RectTransform rt = img.GetComponent<RectTransform>();
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, heartIconSize.x);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heartIconSize.y);

            heartImgs.Add(img);
        }
    }

    // Update each heart sprite based on HP
    void RefreshHearts()
    {
        for (int i = 0; i < heartImgs.Count; i++)
        {
            bool filled = i < currentHealth;
            Image img = heartImgs[i];
            if (img == null) continue;

            img.sprite = filled ? heartFull : heartEmpty;
        }
    }

    // Optional: trigger victory when needed
    public void TriggerVictory()
    {
        if (wendySaved) return;
        wendySaved = true;

        if (victoryBackground != null)
            victoryBackground.SetActive(true);

        if (victoryText != null)
        {
            victoryText.SetActive(true);
            TextMeshProUGUI text = victoryText.GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = "Wendy Saved!";
                text.color = Color.black;
            }
        }

        Time.timeScale = 0f; // pause game
    }
}