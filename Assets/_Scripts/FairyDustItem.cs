using UnityEngine;
using UnityEngine.SceneManagement;

public class FairyDustItem : MonoBehaviour
{
    [HideInInspector] public FairyDustSpawner spawner;

    [Header("Follow Settings")]
    public float followDistance = 1.5f;
    public float followSpeed = 3f;

    [Header("Lifetime Before Collected")]
    public bool autoDestroyEnabled = false;
    private float destroyTime;

    [Header("Delivery Settings")]
    public Vector2 targetPosition = new Vector2(5f, -2f);
    public float deliveryRadius = 1.0f;
    public GameObject deliveredIconPrefab;
    public Vector2 baseIconPosition = new Vector2(5f, -2f);
    public float yOffsetPerDelivery = 0.5f;
    public int maxDeliveries = 5;

    private bool isFollowing = false;
    private Transform followTarget;

    // shared among all Fairy Dusts
    private static int deliveredCount = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void ResetCounterOnSceneLoad()
    {
        deliveredCount = 0;
        Debug.Log("🔄 FairyDust delivered count reset after scene load.");
    }

    public static void ResetDeliveredCount()
    {
        deliveredCount = 0;
        Debug.Log("🔁 FairyDust delivered count manually reset by GameOverManager.");
    }

    void Update()
    {
        if (isFollowing && followTarget != null)
        {
            Vector3 targetPos = followTarget.position + new Vector3(-followDistance, 0.8f, 0);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * followSpeed);

            if (Vector2.Distance(transform.position, targetPosition) < deliveryRadius)
            {
                DeliverAndDisappear();
            }
        }
        else
        {
            transform.position += Vector3.up * Mathf.Sin(Time.time * 2f) * 0.0005f;

            if (autoDestroyEnabled && Time.time > destroyTime)
                Destroy(gameObject);
        }
    }

    public void SetLifetime(float seconds)
    {
        autoDestroyEnabled = true;
        destroyTime = Time.time + seconds;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            followTarget = other.transform;
            isFollowing = true;
            spawner?.NotifyCollected();
            autoDestroyEnabled = false;
        }
    }

    void DeliverAndDisappear()
    {
        if (deliveredCount < maxDeliveries)
        {
            Vector2 iconPos = baseIconPosition + Vector2.up * (yOffsetPerDelivery * deliveredCount);

            if (deliveredIconPrefab != null)
                Instantiate(deliveredIconPrefab, iconPos, Quaternion.identity);

            deliveredCount++;
            Debug.Log($"✨ Delivered #{deliveredCount}");
        }

        // 🟢 when collected all 5 dusts, decide which ending to load
        if (deliveredCount >= maxDeliveries)
        {
            if (BossPeter.wasHit)
            {
                Debug.Log("🌈 All Fairy Dusts delivered! Loading End scene...");
                SceneManager.LoadScene("End");
            }
            else
            {
                Debug.Log("🌟 Perfect Run! No hits taken! Loading EndSkip scene...");
                SceneManager.LoadScene("EndSkip");
            }
            return;
        }

        spawner?.NotifyCollected();
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(targetPosition, deliveryRadius);

        Gizmos.color = Color.yellow;
        for (int i = 0; i < maxDeliveries; i++)
        {
            Vector2 pos = baseIconPosition + Vector2.up * (yOffsetPerDelivery * i);
            Gizmos.DrawSphere(pos, 0.05f);
        }
    }
}