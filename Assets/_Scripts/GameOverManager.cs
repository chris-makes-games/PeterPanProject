using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI; // Reference to the GameOver Canvas

    public void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Game Over UI shown!");
        }
        else
        {
            Debug.LogError("❌ GameOver UI is not assigned!");
        }
    }

    public void RestartGame()
    {
        // ✅ Unpause game
        Time.timeScale = 1f;

        // ✅ Reset Fairy Dust progress counter before reloading
        FairyDustItem.ResetDeliveredCount();

        // ✅ Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}