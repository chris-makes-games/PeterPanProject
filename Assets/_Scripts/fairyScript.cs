using UnityEngine;

public class fairyScript : MonoBehaviour
{
    public float fairySpeed = 12f;
    public float minHeight = 0;
    public float maxHeight = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(transform.position.x, Random.Range(minHeight, maxHeight));
    }

    // Update is called once per frame
    void Update()
    {
        //move fairy and branch/leave children to the left at speed
        transform.position += Time.deltaTime * fairySpeed * Vector3.left;
    }
}
