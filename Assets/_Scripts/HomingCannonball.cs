using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingCannonball : MonoBehaviour
{
    [Header("Homing Settings")]
    public Transform target;
    public float moveSpeed = 4f;
    public float rotateSpeed = 200f;

    [Header("Trail Settings")]
    public bool enableTrail = true;      // toggle trail effect
    public Gradient trailColor;          // gradient over lifetime
    public float trailTime = 0.4f;       // how long the trail lasts
    public float trailWidth = 0.2f;      // width of the trail

    private Rigidbody2D rb;
    private TrailRenderer trail;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Optionally add trail dynamically
        if (enableTrail)
        {
            trail = gameObject.AddComponent<TrailRenderer>();

            // Basic style
            trail.time = trailTime;
            trail.startWidth = trailWidth;
            trail.endWidth = 0f;
            trail.autodestruct = false;

            // Apply color gradient
            if (trailColor == null || trailColor.colorKeys.Length == 0)
            {
                // Default fiery color gradient (red→orange→yellow→transparent)
                GradientColorKey[] colorKeys = new GradientColorKey[3];
                colorKeys[0].color = Color.red;
                colorKeys[0].time = 0f;
                colorKeys[1].color = new Color(1f, 0.6f, 0f);
                colorKeys[1].time = 0.5f;
                colorKeys[2].color = Color.yellow;
                colorKeys[2].time = 1f;

                GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
                alphaKeys[0].alpha = 1f;
                alphaKeys[0].time = 0f;
                alphaKeys[1].alpha = 0f;
                alphaKeys[1].time = 1f;

                trailColor = new Gradient();
                trailColor.SetKeys(colorKeys, alphaKeys);
            }

            trail.colorGradient = trailColor;

            // Make the trail additive (for glow)
            Material mat = new Material(Shader.Find("Sprites/Default"));
            mat.color = new Color(1f, 0.6f, 0f, 1f);
            trail.material = mat;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Direction toward target
        Vector2 dir = ((Vector2)target.position - rb.position).normalized;

        // Rotate smoothly
        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        // Move forward
        rb.linearVelocity = transform.up * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("💥 Homing cannonball hit the player!");
        }

        // Destroy both the missile and its trail
        if (trail != null)
            trail.autodestruct = true;

        Destroy(gameObject);
    }
}