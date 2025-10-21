using UnityEngine;

public class BossPeter : MonoBehaviour
{
    [Header("Movement Settings")]
    public float flySpeed = 5f;
    private Rigidbody2D rb;
    private float horizontalInput, verticalInput;

    private Camera mainCam;
    private float minX, maxX, minY, maxY;

    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;
    private bool isFalling = false;

    private GameUIManager uiManager;
    private GameOverManager gameOverManager;
    private bool facingRight = true;

    [Header("Game Over Settings")]
    public float fallYLimit = -6f;

    [Header("Fairy Dust Settings")]
    public GameObject fairyDustPrefab;
    public Transform firePoint;       
    public float shootCooldown = 0.5f;
    private float lastShootTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        UpdateBounds();
        currentHealth = maxHealth;

        uiManager = FindFirstObjectByType<GameUIManager>();
        gameOverManager = FindFirstObjectByType<GameOverManager>();

        uiManager?.UpdateHealth(currentHealth, maxHealth);
    }

    void FixedUpdate()
    {
        if (isFalling) return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(horizontalInput * flySpeed, verticalInput * flySpeed);

        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight)) Flip();

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void Update()
    {
        if (isFalling && transform.position.y < fallYLimit)
        {
            gameOverManager?.ShowGameOver();
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastShootTime > shootCooldown)
        {
            ShootFairyDust();
            lastShootTime = Time.time;
        }
    }

    void ShootFairyDust()
    {
        if (fairyDustPrefab == null || firePoint == null)
        {
            Debug.LogWarning("⚠️ No Fairy Dust Prefab or FirePoint set!");
            return;
        }

        GameObject dust = Instantiate(fairyDustPrefab, firePoint.position, Quaternion.identity);

        Rigidbody2D rbDust = dust.GetComponent<Rigidbody2D>();
        if (rbDust != null)
        {
            rbDust.gravityScale = 2f;
            rbDust.linearVelocity = Vector2.down * 2f;
        }

        Debug.Log("✨ Fairy Dust success！");
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
        if (collision.gameObject.CompareTag("Cannonball")) TakeDamage(1);
    }

    void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        uiManager?.UpdateHealth(currentHealth, maxHealth);
        if (currentHealth <= 0 && !isFalling) FallAndDie();
    }

    void FallAndDie()
    {
        isFalling = true;
        rb.gravityScale = 5f;
        rb.linearVelocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, 0, 180f);
        rb.angularVelocity = 500f;
    }
}