using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    [Header("Cannon Settings")]
    public GameObject cannonballPrefab;       // Normal cannonball prefab
    public GameObject homingCannonballPrefab; // Homing cannonball prefab
    public float fireRate = 2f;               // Interval between volleys
    public float speed = 6f;                  // Normal shot speed
    public float destroyTime = 5f;            // Auto destroy after X seconds
    public float baseAngleVariation = 5f;     // Small random offset for realism

    [Header("Multi-Shot Settings")]
    public int projectileCount = 3;           // How many cannonballs in multi-shot
    public float spreadAngle = 15f;           // Angle between cannonballs
    [Range(0f, 1f)]
    public float multiShotChance = 0.2f;      // Chance (0-1) for triple shot

    [Header("Homing Settings")]
    [Range(0f, 1f)]
    public float homingChance = 0.1f;         // Chance (0-1) for homing missile
    public float homingSpeed = 4f;            // Homing missile speed
    public float rotationSpeed = 200f;        // How fast it turns toward player

    void Start()
    {
        StartCoroutine(FireLoop());
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Fire()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("⚠️ Player not found!");
            return;
        }

        Vector2 firePos = transform.position;
        Vector2 baseDir = (player.transform.position - (Vector3)firePos).normalized;

        // 🎯 Determine which mode to use
        float roll = Random.value;
        if (roll < homingChance)
        {
            FireHoming(firePos, player.transform);
        }
        else if (roll < homingChance + multiShotChance)
        {
            FireMultiple(firePos, baseDir);
        }
        else
        {
            FireSingle(firePos, baseDir);
        }
    }

    void FireSingle(Vector2 firePos, Vector2 baseDir)
    {
        float randomOffset = Random.Range(-baseAngleVariation, baseAngleVariation);
        Vector2 dir = Quaternion.Euler(0, 0, randomOffset) * baseDir;
        SpawnBall(cannonballPrefab, firePos, dir, speed);
    }

    void FireMultiple(Vector2 firePos, Vector2 baseDir)
    {
        int middle = projectileCount / 2;
        for (int i = 0; i < projectileCount; i++)
        {
            float offset = (i - middle) * spreadAngle + Random.Range(-baseAngleVariation, baseAngleVariation);
            Vector2 dir = Quaternion.Euler(0, 0, offset) * baseDir;
            SpawnBall(cannonballPrefab, firePos, dir, speed);
        }
        Debug.Log($"💥 Multi-shot triggered! {projectileCount} balls fired!");
    }

    void FireHoming(Vector2 firePos, Transform target)
    {
        if (homingCannonballPrefab == null)
        {
            Debug.LogWarning("⚠️ No homing cannonball prefab assigned!");
            return;
        }

        GameObject homing = Instantiate(homingCannonballPrefab, firePos, Quaternion.identity);
        HomingCannonball hc = homing.GetComponent<HomingCannonball>();
        if (hc != null)
        {
            hc.target = target;
            hc.moveSpeed = homingSpeed;
            hc.rotateSpeed = rotationSpeed;
        }

        Destroy(homing, destroyTime);
        Debug.Log("🎯 Homing cannonball launched!");
    }

    void SpawnBall(GameObject prefab, Vector2 pos, Vector2 dir, float spd)
    {
        if (prefab == null) return;

        GameObject ball = Instantiate(prefab, pos, Quaternion.identity);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = dir * spd;

        Destroy(ball, destroyTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}