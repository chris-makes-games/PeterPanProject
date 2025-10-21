using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    [Header("Cannon Settings")]
    public GameObject cannonballPrefab;   // The cannonball prefab
    public float fireRate = 2f;           // Interval between shots (seconds)
    public float speed = 6f;              // Cannonball speed
    public float destroyTime = 5f;        // Lifetime before auto-destroy
    public float angleVariation = 10f;    // Random firing angle in degrees

    // Fixed world position to spawn cannonballs
    private readonly Vector3 fixedSpawnPosition = new Vector3(2.36f, -1.24f, 0f);

    void Start()
    {
        // Start repeating fire loop
        StartCoroutine(FireLoop());
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            FireOnce();
            yield return new WaitForSeconds(fireRate); // Wait between shots
        }
    }

    void FireOnce()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null || cannonballPrefab == null)
        {
            Debug.LogWarning("Cannonball prefab or Player not found!");
            return;
        }

        // Calculate direction toward the player
        Vector2 firePos = fixedSpawnPosition;
        Vector2 direction = (player.transform.position - (Vector3)firePos).normalized;

        // Add small random angle for more natural effect
        float randomAngle = Random.Range(-angleVariation, angleVariation);
        direction = Quaternion.Euler(0, 0, randomAngle) * direction;

        // Instantiate cannonball
        GameObject newBall = Instantiate(cannonballPrefab, firePos, Quaternion.identity);

        // Apply velocity
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }

        // Destroy after a few seconds
        Destroy(newBall, destroyTime);
    }

    // Draw a red dot in Scene view to show spawn point
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(-0.8999f, -1.324f, 0f), 0.1f);
    }
}