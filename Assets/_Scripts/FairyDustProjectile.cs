using UnityEngine;

public class FairyDustProjectile : MonoBehaviour
{
    [Header("Physics Settings")]
    public float gravityScale = 2f;
    public float initialDownForce = 2f;

    private Rigidbody2D rb;
    private GameUIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.linearVelocity = Vector2.down * initialDownForce;

        uiManager = FindFirstObjectByType<GameUIManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DestroyZone"))
        {
            Debug.Log("💥 Fairy Dust destroyed by Destroy Zone!");

            if (uiManager != null)
            {
                uiManager.AddFairyDust(1);
            }

            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}