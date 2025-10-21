using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class fairyScript : MonoBehaviour
{
    public float fairySpeed = 12f;
    public float minHeight = 0;
    public float maxHeight = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(minHeight, maxHeight)); //random position

        //random hue near to purple
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Light2D light = GetComponent<Light2D>();
        float red = Random.Range(.12f, 1f);
        float green = Random.Range(0, .12f);
        float blue = Random.Range(.12f, 1f);
        //sets color
        spriteRenderer.color = new Color(red, green, blue);
        light.color = new Color(red, green, blue);
    }

    // Update is called once per frame
    void Update()
    {
        //move fairy and branch/leave children to the left at speed
        transform.position += Time.deltaTime * fairySpeed * Vector3.left;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TreeDestroy") || collision.CompareTag("Player"))
        {
            Destroy(gameObject); //destroys self when passing into destroy area or collected by player
        }
    }

    public void increaseSpeed(float speed)
    {
        fairySpeed += speed * Time.deltaTime;
    }

    public void setSpeed(float speed)
    {
        fairySpeed = speed;
    }
}

