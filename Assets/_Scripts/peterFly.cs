using UnityEngine;

public class peterFly : MonoBehaviour
{
    public float flySpeed;

    Rigidbody2D rb;

    float horizontalInput;
    float verticalInput;

    // Camera reference for bounds
    Camera mainCam;
    float minX, maxX, minY, maxY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;

        // calculate bounds at start
        UpdateBounds();
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput * flySpeed, verticalInput * flySpeed);
        rb.linearVelocity = movement;

        // clamp position to camera bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
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
}
