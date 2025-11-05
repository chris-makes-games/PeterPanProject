using UnityEngine;

public class FairyDustDepositZone : MonoBehaviour
{
    [Header("Zone")]
    public Transform zoneCenter;     // where to place icons around (usually this object's transform)
    public float detectRadius = 1.0f; // pickup delivery radius for distance check (world units)

    [Header("Display")]
    public GameObject depositIconPrefab; // world-space sprite (a small fairy dust icon)
    public float bottomOffset = -0.6f;   // first icon at bottom (relative to center)
    public float verticalSpacing = 0.6f; // distance between stacked icons
    public int capacity = 3;             // we want 3 slots: bottom, middle, top

    private int deposited = 0;

    void Reset()
    {
        if (!zoneCenter) zoneCenter = transform;
    }

    public Vector2 GetCenter() => (zoneCenter ? (Vector2)zoneCenter.position : (Vector2)transform.position);
    public float GetRadius() => detectRadius;

    /// <summary>
    /// Try to deposit one icon. Returns true if placed, false if already full.
    /// </summary>
    public bool TryDeposit()
    {
        if (deposited >= capacity) return false;

        // bottom -> middle -> top
        float y = bottomOffset + deposited * verticalSpacing;
        Vector3 placePos = GetCenter() + new Vector2(0f, y);

        if (depositIconPrefab != null)
        {
            Instantiate(depositIconPrefab, placePos, Quaternion.identity);
        }

        deposited++;
        return true;
    }

    void OnDrawGizmosSelected()
    {
        // visualize delivery radius and three stacked slots
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(GetCenter(), detectRadius);

        Gizmos.color = Color.yellow;
        for (int i = 0; i < capacity; i++)
        {
            float y = bottomOffset + i * verticalSpacing;
            Gizmos.DrawSphere(GetCenter() + new Vector2(0f, y), 0.06f);
        }
    }
}