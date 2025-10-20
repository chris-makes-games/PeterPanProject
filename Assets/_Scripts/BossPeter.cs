using UnityEngine;

public class BossPeter : MonoBehaviour
{
    [Header("Movement Settings")]
    public float flySpeed = 5f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private float verticalInput;

    // Camera reference for bounds
    private Camera mainCam;
    private float minX, maxX, minY, maxY;

    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;
    private bool isFalling = false;

    // Reference to UI Managers
    private GameUIManager uiManager;
    private GameOverManager gameOverManager;

    // For flipping direction
    private bool facingRight = true;

    [Header("Game Over Settings")]
    public float fallYLimit = -6f; // When Peter falls below this Y, trigger Game Over

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        UpdateBounds();
        currentHealth = maxHealth;

        // Find UI Managers in the scene
        uiManager = FindFirstObjectByType<GameUIManager>();
        gameOverManager = FindFirstObjectByType<GameOverManager>();

        // Initialize UI health
        if (uiManager != null)
            uiManager.UpdateHealth(currentHealth, maxHealth);
    }

    void FixedUpdate()
    {
        if (isFalling) return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput * flySpeed, verticalInput * flySpeed);
        rb.linearVelocity = movement;

        // Flip character when changing direction
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();

        // Clamp position to camera bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void Update()
    {
        // Check if Peter has fallen below the Y limit
        if (isFalling && transform.position.y < fallYLimit)
        {
            TriggerGameOverUI();
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void UpdateBounds()
    {
        float camHeight = mainCam.orthographicSize;
        float camWidth = camHeight * mainCam.aspect;

        minX = mainCam.transform.position.x - camWidth;
        maxX = mainCam.transform.position.x + camWidth;
        minY = mainCam.transform.position.y - camHeight;
        maxY = mainCam.transform.position.y + camHeight;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cannonball"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log($"Peter Pan took {amount} damage! Remaining health: {currentHealth}");

        if (uiManager != null)
            uiManager.UpdateHealth(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isFalling)
            FallAndDie();
    }

    void FallAndDie()
    {
        Debug.Log("Peter Pan has been defeated and is falling!");

        isFalling = true;

        // Enable gravity and increase fall speed
        rb.gravityScale = 5f;
        rb.linearVelocity = Vector2.zero;

        // Flip upside down visually
        transform.rotation = Quaternion.Euler(0, 0, 180f);

        // Add spin for dramatic fall
        rb.angularVelocity = 500f;
    }

    void TriggerGameOverUI()
    {
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
            Debug.Log("✅ Game Over triggered: Peter fell off-screen!");
        }
        else
        {
            Debug.LogError("❌ GameOverManager not found in scene!");
        }
    }
}