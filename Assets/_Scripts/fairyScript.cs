using UnityEngine;

public class fairyScript : MonoBehaviour
{
    public float fairySpeed = 12f;
    public float minHeight = 0;
    public float maxHeight = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(minHeight, maxHeight)); //random position
    }

    // Update is called once per frame
    void Update()
    {
        //move fairy and branch/leave children to the left at speed
        transform.position += Time.deltaTime * fairySpeed * Vector3.left;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TreeDestroy"))
        {
            Destroy(gameObject); //destroys self when passing into destroy area
        }
    }

    //getters and setters for difficulty curve
    public void increaseSpeed(float speed)
    {
        fairySpeed += speed * Time.deltaTime;
    }

    public void setSpeed(float speed)
    {
        fairySpeed = speed;
    }

    public float getSpeed()
    {
        return fairySpeed;
    }
}

