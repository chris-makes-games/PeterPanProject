using UnityEngine;
using System;

public class FairyDust : MonoBehaviour
{
    private Action onDestroyCallback;
    private bool collected = false; // Prevent multiple triggers
    private bool destroyedByPlayer = false; // Flag to check if destroyed by player

    public void Init(Action callback)
    {
        onDestroyCallback = callback;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore duplicate triggers
        if (collected) return;

        if (other.CompareTag("Player"))
        {
            collected = true;
            destroyedByPlayer = true; // Mark as destroyed by player
            Debug.Log("Fairy Dust collected!");

            // Update UI once
            GameUIManager uiManager = FindFirstObjectByType<GameUIManager>();
            if (uiManager != null)
                uiManager.AddDust(1);

            // Destroy Fairy Dust object
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Only call the callback when destroyed naturally (not by collection)
        if (!destroyedByPlayer)
        {
            onDestroyCallback?.Invoke();
        }
    }
}