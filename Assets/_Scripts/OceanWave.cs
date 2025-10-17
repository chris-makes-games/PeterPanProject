using UnityEngine;

public class WaveScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Speed of scrolling waves

    private float waveWidth;        // Width of the wave sprite
    private Vector3 startPos;       // Initial position

    void Start()
    {
        // Get the width of the sprite in world units
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        waveWidth = sr.bounds.size.x;
        startPos = transform.position;
    }

    void Update()
    {
        // Loop the X position using Mathf.Repeat
        float newX = Mathf.Repeat(Time.time * scrollSpeed, waveWidth);
        transform.position = startPos + Vector3.left * newX;
    }
}
