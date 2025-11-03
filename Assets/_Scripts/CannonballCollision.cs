using UnityEngine;

public class CannonballCollision : MonoBehaviour
{
    private float spawnTime;
    public float selfCollisionIgnoreTime = 0.1f; // seconds to ignore cannonball-cannonball hits

    void Start()
    {
        spawnTime = Time.time;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string otherTag = collision.gameObject.tag;

        // Ignore cannonball vs cannonball collisions right after spawn
        if (otherTag == "Cannonball" && Time.time - spawnTime < selfCollisionIgnoreTime)
            return;

        // 💥 Hit the player
        if (otherTag == "Player")
        {
            Destroy(gameObject);
        }
        // 💥 Hit an obstacle
        else if (otherTag == "Obstacle")
        {
            Destroy(gameObject);
        }
        // 💣 Hit another cannonball → destroy both
        else if (otherTag == "Cannonball")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}