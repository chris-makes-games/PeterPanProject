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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}