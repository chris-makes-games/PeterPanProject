using UnityEngine;

public class shipMove : MonoBehaviour
{
    public float speed;
    public float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < distance)
        {
            transform.position += Time.deltaTime * speed * Vector3.right;
        }
    }
}
