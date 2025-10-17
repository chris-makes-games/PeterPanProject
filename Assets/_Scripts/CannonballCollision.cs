using UnityEngine;

public class CannonballCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the cannonball hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Cannonball hit Peter Pan!");

            // TODO: you can add damage logic here
            // e.g. collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);

            // Destroy the cannonball after hitting the player
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Optional: if it hits something else like a tree or rock
            Destroy(gameObject);
        }
    }
}