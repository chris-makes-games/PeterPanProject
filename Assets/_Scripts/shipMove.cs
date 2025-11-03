using UnityEngine;

public class shipMove : MonoBehaviour
{
    public float speed;
    public float distance;
    public GameObject cannon;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < distance)
        {
            
            transform.position += Time.deltaTime * speed * Vector3.right;
        }
        else if (cannon.activeSelf == false)
        {
            cannon.SetActive(true); //activates cannon once it is at the position
        }
    }
}
