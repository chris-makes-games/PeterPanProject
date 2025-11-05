using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("Hearts UI")]
    public Transform heartsParent;      // parent transform that holds all hearts
    public GameObject heartPrefab;      // prefab for one heart icon
    public Sprite heartFull;            // sprite for full heart
    public Sprite heartEmpty;           // sprite for empty heart

    [Header("Victory UI (Optional)")]
    public GameObject victoryBackground;
    public GameObject victoryText;

    private int currentHealth = 5;
    private int maxHealth = 5;

    private readonly List<Image> heartImgs = new();

    void Start()
    {
        // create hearts initially
        RebuildHearts(maxHealth);
        RefreshHearts();

        // hide victory screen if exists
        if (victoryBackground != null) victoryBackground.SetActive(false);
        if (victoryText != null) victoryText.SetActive(false);
    }

    // 🟢 called by BossPeter when HP changes
    public void UpdateHealth(int newHealth, int max)
    {
        maxHealth = max;
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);

        // if number of hearts changed, rebuild them
        if (heartImgs.Count != maxHealth)
            RebuildHearts(maxHealth);

        RefreshHearts();
    }

    // create heart icons
    void RebuildHearts(int count)
    {
        foreach (Transform child in heartsParent)
            Destroy(child.gameObject);

        heartImgs.Clear();

        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(heartPrefab, heartsParent);
            heartImgs.Add(go.GetComponent<Image>());
        }
    }

    // refresh heart sprites to reflect current health
    void RefreshHearts()
    {
        for (int i = 0; i < heartImgs.Count; i++)
        {
            heartImgs[i].sprite = (i < currentHealth) ? heartFull : heartEmpty;
        }
    }
}