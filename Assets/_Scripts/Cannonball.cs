using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Cannonball : MonoBehaviour
{
    public float speed = 5f;       // Speed of cannonball
    public float lifeTime = 5f;    // Auto destroy after some time

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime); // cleanup after lifetime
    }

    // Set direction once at spawn
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Damage player (if you have health system)
            var player = other.GetComponent<peterFly>();
            // 如果你以后给玩家加 HP，可以在这里调用 TakeDamage()

            Destroy(gameObject); // Destroy on hit
        }
    }
}