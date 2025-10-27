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
        if (health < 0)  //avoid out of range index during additional damage
        {
            jitter();
            return;
        }
        healthbarSprite.sprite = healthSprites[health];
        jitter();
    }

    //jitters around three times when health goes down
    void jitter()
    {
        StartCoroutine(jitterWait());
        StartCoroutine(jitterWait());
        StartCoroutine(jitterWait());
    }

    Vector2 randomXY() // returns random xy position
    {
        Vector2 newPosition = new Vector2(Random.Range(jitterAmount, jitterAmount + .5f), Random.Range(jitterAmount, jitterAmount + .5f));
        return startPosition + newPosition;
    }

    IEnumerator jitterWait()
    {
        transform.position = randomXY();
        yield return new WaitForSeconds(0.1f); // short wait
        transform.position = startPosition;
    }
}
