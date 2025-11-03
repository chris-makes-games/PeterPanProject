using UnityEngine;
using System.Collections;

public class peterFly : MonoBehaviour
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
    private bool isInvincible = false;
    private float invincibleDuration = 2f;

    //health bar stuff
    public GameObject healthBar;
    private healthUpdate healthScript;

    //used for color change
    SpriteRenderer spriteRenderer;
    Color normal = new Color(1f, 1f, 1f);
    Color damaged = new Color(1f, .3f, .3f);

    //used to manage difficulty curve
    public GameObject difficultyManager;
    private difficultyCurve curve;

    // Reference to Game UI Manager
    private GameUIManager uiManager;
    public GameOverManager gameOver;

    // For flipping direction
    private bool facingRight = true; // true = facing right, false = facing left
    public bool isFlippable = true;

    //object for spawning fairy dust
    public GameObject fairyDust;

    void Start()
    {
        //script access for difficulty
        curve = difficultyManager.GetComponent<difficultyCurve>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        //health script
        healthScript = healthBar.GetComponent<healthUpdate>();
        
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        UpdateBounds();
        currentHealth = maxHealth;

        // Find the UI manager in the scene
        uiManager = FindFirstObjectByType<GameUIManager>();

        // Initialize UI health
        if (uiManager != null)
            uiManager.UpdateHealth(currentHealth, maxHealth);
    }

    void FixedUpdate()
    {
        if (isFalling) return; // Disable movement when falling

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput * flySpeed, verticalInput * flySpeed);
        if (!isFlippable && movement.x < 0)//if peter is going left in chase scene
        {
            movement.x *= 1.5f; //go left 50% faster
        }
        rb.linearVelocity = movement;

        // Flip character when changing direction
        // adding logic to prevent flip for chase scene
        if (isFlippable && horizontalInput > 0 && !facingRight)
            Flip();
        else if (isFlippable && horizontalInput < 0 && facingRight)
            Flip();

        // Clamp position to camera bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void Flip()
    {
        // Reverse facing direction
        facingRight = !facingRight;

        // Flip sprite horizontally by changing local scale
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
        if (collision.gameObject.CompareTag("Cannonball") || collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1); //takes damage on cannonball or obstacle
        }       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fairy"))
        {
            curve.increaseDifficulty();
            curve.fairyCollected();
            Destroy(collision.gameObject); //destroys fairy
            generateDust();
        }
    }

    void generateDust() //generates 6 dust particles 
    {
        //I tried to get dust to spawn here but I wasn't sure how - Chris

    }

    void TakeDamage(int amount)
    {
        if(isInvincible) return;//stops damage if player is invincible

        
        currentHealth -= amount; //lowers HP
        healthScript.changeSprite(currentHealth);
        curve.decreaseDifficulty(); //lowers difficulty if player gets hit
        Debug.Log($"Peter Pan took {amount} damage! Remaining health: {currentHealth}");

        // Update UI health display
        if (uiManager != null)
        {
            uiManager.UpdateHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0 && !isFalling)
        {
            FallAndDie();
        }
        else
        {
            StartCoroutine(BecomeTemporarilyInvincible()); //makes player invincible
        }
    }

    void FallAndDie()
    {
        Debug.Log("Peter Pan has been defeated and is falling!");

        isFalling = true;

        // Enable gravity and increase fall speed
        rb.gravityScale = 5f;

        // Stop flying movement
        rb.linearVelocity = Vector2.zero;

        // Flip upside down visually
        transform.rotation = Quaternion.Euler(0, 0, 180f);

        // Add spin for dramatic fall
        rb.angularVelocity = 500f;

        //shows game over screen after 2.5seconds
        StartCoroutine(gameOverWait());

    }

    IEnumerator gameOverWait()
    {
        yield return new WaitForSeconds(2.5f); // Waits for specified seconds
        gameOver.ShowGameOver();
    }

    // using this tutorial for i-frames: https://www.aleksandrhovhannisyan.com/blog/invulnerability-frames-in-unity
    //player goes invulnarable for fixed duration after taking damage
    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        spriteRenderer.color = damaged;
        yield return new WaitForSeconds(invincibleDuration); //invinvible for fixed duration
        isInvincible = false;
        if (currentHealth > 0) //change back to normal sprite unless dead
        {
            spriteRenderer.color = normal;
        }
        
    }

    public int getPlayerHealth()
    {
        return currentHealth;
    }
}
