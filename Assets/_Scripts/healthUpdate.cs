using UnityEngine;
using System.Collections;

public class healthUpdate : MonoBehaviour
{
    public Sprite[] healthSprites;
    private SpriteRenderer healthbarSprite;
    private Vector2 startPosition;
    public float jitterAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthbarSprite = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }

    public void changeSprite(int health)
    {
        if (health >= 0)  //avoid out of range index during additional damage
        {
            healthbarSprite.sprite = healthSprites[health];
        }
        StartCoroutine(jitterWait());
    }

    Vector2 randomXY() // returns random xy position
    {
        Vector2 newPosition = new Vector2(jitterAmount, jitterAmount);
        return startPosition + newPosition;
    }

    IEnumerator jitterWait()
    {
        transform.position = randomXY();
        yield return new WaitForSeconds(0.1f); // short wait
        transform.position = startPosition;
    }
}
