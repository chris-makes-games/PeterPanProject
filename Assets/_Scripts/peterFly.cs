using UnityEngine;

public class peterFly : MonoBehaviour
{
    public float flySpeed;

    Rigidbody2D rb;

    float horizontalInput;
    float verticalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput * flySpeed, verticalInput * flySpeed);
        rb.linearVelocity = movement;
    }
}
