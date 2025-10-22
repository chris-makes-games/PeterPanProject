using UnityEngine;

public class chasecannonfire : MonoBehaviour
{
    public float speed = 5f;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(-27, 14); //top left of screen
        float randomChange1 = Random.Range(0, 10);
        float randomChange2 = Random.Range(0, 10);
        Vector2 modifiedDirection = new Vector2(randomChange1, randomChange2);
        Vector2 finalDirection = direction + modifiedDirection;
        rb.linearVelocity = finalDirection * speed;
        Destroy(gameObject, 5f); //destroys after 5 seconds

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy if the cannonball hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Fairy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
