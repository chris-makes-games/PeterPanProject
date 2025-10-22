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
        Vector2 direction = player.transform.position - transform.position;
        rb.linearVelocity = direction * speed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
